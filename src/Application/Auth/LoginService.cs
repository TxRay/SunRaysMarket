using Application.DomainModels;
using Application.Repositories;
using Application.Results;
using FluentValidation;

namespace Application.Auth;

internal class LoginService(
    IValidator<LoginModel> loginModelValidator,
    IUserRepository userRepository
) : ILoginService
{
    public async Task<AuthResult> LoginAsync(LoginModel loginModel)
    {
        var user = await userRepository.AuthenticateAsync(loginModel);

        return user is null ? AuthResult.Failure("Invalid credentials.") : AuthResult.Success(user);
    }
}
