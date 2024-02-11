using FluentValidation;
using SunRaysMarket.Server.Application.Repositories;
using SunRaysMarket.Server.Application.Results;
using SunRaysMarket.Server.Application.Services.Auth;

namespace SunRaysMarket.Server.Application.ServicesImpl.Scoped.Auth;

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
