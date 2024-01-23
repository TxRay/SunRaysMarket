using SunRaysMarket.Server.Application.Results;

namespace SunRaysMarket.Server.Application.Auth;

public interface ISignUpService
{
    Task<AuthResult> CustomerSignUpAsync(SignUpModel signUpModel, IEnumerable<Role> roles);
}
