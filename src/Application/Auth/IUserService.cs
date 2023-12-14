using System.Security.Claims;
using Application.DomainModels;
using Application.Results;
using FluentValidation.Results;

namespace Application.Auth;

public interface IUserService
{
    Task<UserDetailsModel?> GetCurrentUserAsync(ClaimsPrincipal? claimsPrincipal);
    Task<AuthResult> LoginAsync(LoginModel loginModel);
    Task<AuthResult> SignUpAsync(SignUpModel signUpModel, IEnumerable<Role> roles);
    Task<AuthResult> UpdateAsync(UpdateUserModel user);
    Task LogoutAsync();
    Task<bool> IsLoggedInAsync();
}
