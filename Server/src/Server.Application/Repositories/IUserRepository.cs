using SunRaysMarket.Server.Application.Enums;
using SunRaysMarket.Server.Application.Services.Auth;

namespace SunRaysMarket.Server.Application.Repositories;

public interface IUserRepository
{
    Task<UserDetailsModel?> GetUserByIdAsync(int id);
    Task<UserDetailsModel?> GetUserByEmailAsync(string email);
    Task<IEnumerable<UserListModel>> GetUsersAsync();
    Task<UserDetailsModel?> CreateUserAsync(SignUpModel model);
    Task AddUserRolesAsync(int userId, IEnumerable<Role> roles);
    Task UpdateUserAsync(UpdateUserModel model);
    Task DeleteUserAsync(int id);
    Task<bool> HasRolesAsync(int userId, Role[] roles);
    Task<bool> HasRoleAsync(int userId, Role role);
    Task<UserDetailsModel?> AuthenticateAsync(LoginModel model);

    Task LogoutAsync();
}