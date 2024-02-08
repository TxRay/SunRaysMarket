using SunRaysMarket.Server.Application.Results;

namespace SunRaysMarket.Server.Application.Services.Auth;

public interface ILoginService
{
    public Task<AuthResult> LoginAsync(LoginModel loginModel);
}
