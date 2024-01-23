using SunRaysMarket.Server.Application.Results;

namespace SunRaysMarket.Server.Application.Auth;

public interface ILoginService
{
    public Task<AuthResult> LoginAsync(LoginModel loginModel);
}
