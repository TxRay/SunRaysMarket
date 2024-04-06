using SunRaysMarket.Server.Application.Results;

namespace SunRaysMarket.Server.Application.Services.Auth;

public interface ILoginService
{
    public Task<AuthResult.AuthSome> LoginAsync(LoginModel loginModel);
}
