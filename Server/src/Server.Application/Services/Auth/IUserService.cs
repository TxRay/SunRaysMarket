using System.Security.Claims;
using SunRaysMarket.Server.Application.Enums;
using SunRaysMarket.Server.Application.Results;

namespace SunRaysMarket.Server.Application.Services.Auth;

public interface IUserService
{
    Task<UserDetailsModel?> GetCurrentUserAsync(ClaimsPrincipal? claimsPrincipal);
    Task<AuthResult> LoginAsync(LoginModel loginModel);
    Task<AuthResult> LoginAdminAsync(LoginModel loginModel);
    Task<AuthResult> SignUpAsync(SignUpModel signUpModel, IEnumerable<Role> roles);
    Task<AuthResult> UpdateAsync(UpdateUserModel user);
    Task LogoutAsync();
    Task<bool> IsLoggedInAsync();
}
