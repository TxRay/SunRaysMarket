using Application.Auth;
using Application.DomainModels;

namespace Application.Repositories;

public interface IUserRepository
{
    Task<UserDetailsModel?> GetUserByIdAsync(int id);
    Task<UserDetailsModel?> GetUserByEmailAsync(string email);
    Task<IEnumerable<UserListModel>> GetUsersAsync();
    Task<UserDetailsModel?> CreateUserAsync(SignUpModel model);
    Task AddUserRolesAsync(int userId, IEnumerable<Role> roles);
    Task UpdateUserAsync(UpdateUserModel model);
    Task DeleteUserAsync(int id);

    Task<UserDetailsModel?> AuthenticateAsync(LoginModel model);

    Task LogoutAsync();
}
