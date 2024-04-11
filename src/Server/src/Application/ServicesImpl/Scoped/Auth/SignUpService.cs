using FluentValidation;
using Microsoft.Extensions.Logging;
using SunRaysMarket.Server.Application.Repositories;
using SunRaysMarket.Server.Application.UnitOfWork;
using SunRaysMarket.Server.Core.DomainModels;
using SunRaysMarket.Server.Core.Enums;
using SunRaysMarket.Server.Core.Results;
using SunRaysMarket.Server.Core.Services.Auth;

namespace SunRaysMarket.Server.Application.ServicesImpl.Scoped.Auth;

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
