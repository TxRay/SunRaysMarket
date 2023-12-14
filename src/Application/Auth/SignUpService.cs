using System.Linq.Expressions;
using Application.DomainModels;
using Application.Repositories;
using Application.Results;
using Application.UnitOfWork;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Application.Auth;

internal class SignUpService(
    ILogger<SignUpService> logger,
    IValidator<SignUpModel> validator,
    IUnitOfWork unitOfWork,
    IUserRepository userRepository
) : ISignUpService
{
    public async Task<AuthResult> CustomerSignUpAsync(
        SignUpModel signUpModel,
        IEnumerable<Role> roles
    )
    {
        logger.LogInformation("Creating user with email {Email}", signUpModel.Email);

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
}
