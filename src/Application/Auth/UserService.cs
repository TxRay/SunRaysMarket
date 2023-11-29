using System.Security.Claims;
using Application.DomainModels;
using Application.Repositories;
using Application.Results;
using Application.UnitOfWork;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Application.Auth;

public class UserService(
    ILogger<UserService> logger,
    IUnitOfWork unitOfWork,
    IUserRepository userRepository,
    IValidator<LoginModel> loginModelValidator,
    IValidator<SignUpModel> signUpModelValidator
) : IUserService
{
    public async Task<UserDetailsModel?> GetCurrentUserAsync(ClaimsPrincipal? claimsPrincipal)
    {
        var userEmailAddress = claimsPrincipal?.Identities
            .First()
            .Claims.Where(claim => claim.Type.Equals("UserName"))
            .Select(claim => claim.Value)
            .FirstOrDefault();

        var userMessage = userEmailAddress is null
            ? "No user found."
            : $"User found with email {userEmailAddress}";
        logger.LogInformation(userMessage);

        return userEmailAddress is not null
            ? await userRepository.GetUserByEmailAsync(userEmailAddress)
            : null;
    }

    public async Task<AuthResult> LoginAsync(LoginModel loginModel)
    {
        var validationResult = await loginModelValidator.ValidateAsync(loginModel);

        if (!validationResult.IsValid)
            return AuthResult.Failure(validationResult);

        var user = await userRepository.AuthenticateAsync(loginModel);

        return user is null ? AuthResult.Failure("Invalid credentials.") : AuthResult.Success(user);
    }

    public async Task<AuthResult> SignUpAsync(SignUpModel signUpModel, IEnumerable<Role> roles)
    {
        logger.LogInformation("Creating user with email {Email}", signUpModel.Email);

        var validationResult = await signUpModelValidator.ValidateAsync(signUpModel);

        if (!validationResult.IsValid)
        {
            logger.LogWarning(
                "Failed to create user. Validation errors: {ValidationErrors}",
                validationResult.Errors.Select(e => e.ErrorMessage)
            );
            return AuthResult.Failure(validationResult);
        }

        var user = await userRepository.CreateUserAsync(signUpModel);

        if (user is null)
        {
            logger.LogWarning("Failed to create user.");
            return AuthResult.Failure("Failed to create user.");
        }

        var rolesEnumerable = roles as Role[] ?? roles.ToArray();
        await userRepository.AddUserRolesAsync(user.Id, rolesEnumerable);

        if (rolesEnumerable.Any(r => r == Role.Customer))
        {
            logger.LogInformation(
                "Creating customer for user with email {Email}",
                signUpModel.Email
            );
            await unitOfWork.CustomerRepository.CreateCustomerAsync(user.Id);
            await unitOfWork.SaveChangesAsync();
        }

        logger.LogInformation("Successfully created user with email {Email}", signUpModel.Email);
        return AuthResult.Success(user);
    }

    public Task<AuthResult> UpdateAsync(UpdateUserModel user)
    {
        throw new NotImplementedException();
    }

    public async Task LogoutAsync()
    {
        await userRepository.LogoutAsync();
    }

    public Task<bool> IsLoggedInAsync()
    {
        throw new NotImplementedException();
    }
}
