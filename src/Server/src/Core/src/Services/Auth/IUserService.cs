using System.Security.Claims;
using SunRaysMarket.Server.Core.DomainModels;
using SunRaysMarket.Server.Core.Enums;
using SunRaysMarket.Server.Core.Results;
using SunRaysMarket.Shared.Core.DomainModels;

namespace SunRaysMarket.Server.Core.Services.Auth;

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