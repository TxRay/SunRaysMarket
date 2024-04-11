using SunRaysMarket.Server.Core.Enums;
using SunRaysMarket.Server.Core.Results;
using SunRaysMarket.Shared.Core.DomainModels;

namespace SunRaysMarket.Server.Core.Services.Auth;

public interface ISignUpService
{
    Task<AuthResult> CustomerSignUpAsync(SignUpModel signUpModel, IEnumerable<Role> roles);
}
