using SunRaysMarket.Server.Core.Results;
using SunRaysMarket.Shared.Core.DomainModels;

namespace SunRaysMarket.Server.Core.Services.Auth;

public interface ILoginService
{
    public Task<AuthResult.AuthSome> LoginAsync(LoginModel loginModel);
}
