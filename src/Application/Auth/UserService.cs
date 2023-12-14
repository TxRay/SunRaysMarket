using System.Security.Claims;
using Application.DomainModels;
using Application.DomainModels.Payment;
using Application.Repositories;
using Application.Results;
using Application.Services;
using Application.UnitOfWork;
using Microsoft.Extensions.Logging;

namespace Application.Auth;

internal class UserService(
    ILogger<UserService> logger,
    IPaymentService paymentService,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork
) : IUserService
{
    public async Task<UserDetailsModel?> GetCurrentUserAsync(ClaimsPrincipal? claimsPrincipal)
    {
        var userEmailAddress = claimsPrincipal
            ?.Identities
            .First()
            .Claims
            .Where(claim => claim.Type.Equals(ClaimTypes.Name))
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
        var user = await userRepository.AuthenticateAsync(loginModel);

        return user is null ? AuthResult.Failure("Invalid credentials.") : AuthResult.Success(user);
    }

    public async Task<AuthResult> SignUpAsync(SignUpModel signUpModel, IEnumerable<Role> roles)
    {
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

            if (await unitOfWork.CustomerRepository.CreateCustomerAsync(user.Id))
            {
                await unitOfWork.SaveChangesAsync();

                var customerId = unitOfWork.CustomerRepository.GetPersistedCustomerId() ?? 0;
                var customerDetails = await unitOfWork
                    .CustomerRepository
                    .GetCustomerDetailsAsync(customerId);

                if (customerDetails is not null)
                {
                    var paymentCustomer = new CreatePaymentCustomerModel
                    {
                        Email = customerDetails.Email,
                        Name = $"{customerDetails.FirstName} {customerDetails.LastName}"
                    };

                    var paymentCustomerId = await paymentService.CreateCustomer(paymentCustomer);

                    if (
                        await unitOfWork
                            .CustomerRepository
                            .AddPaymentIdAsync(customerDetails.Id, paymentCustomerId)
                    )
                        await unitOfWork.SaveChangesAsync();
                }
            }
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
