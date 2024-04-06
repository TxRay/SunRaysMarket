using System.Security.Claims;
using SunRaysMarket.Server.Application.Enums;
using SunRaysMarket.Server.Application.Results;

namespace SunRaysMarket.Server.Application.Services.Auth;

public interface IUserService
{
    Task<UserDetailsModel?> GetCurrentUserAsync(ClaimsPrincipal? claimsPrincipal);
    Task<AuthResult.AuthSome> LoginAsync(LoginModel loginModel);
    Task<AuthResult.AuthSome> LoginAdminAsync(LoginModel loginModel);
    Task<AuthResult.AuthSome> SignUpAsync(SignUpModel signUpModel, IEnumerable<Role> roles);
    Task<AuthResult> UpdateAsync(UpdateUserModel user);
    Task LogoutAsync();
    Task<bool> IsLoggedInAsync();
}
