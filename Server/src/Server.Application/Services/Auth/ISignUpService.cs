using SunRaysMarket.Server.Application.Enums;
using SunRaysMarket.Server.Application.Results;

namespace SunRaysMarket.Server.Application.Services.Auth;

public interface ISignUpService
{
    Task<AuthResult> CustomerSignUpAsync(SignUpModel signUpModel, IEnumerable<Role> roles);
}
