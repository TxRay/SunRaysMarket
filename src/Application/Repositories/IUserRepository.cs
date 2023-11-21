using Application.DomainModels;

namespace Application.Repositories;

public interface IUserRepository
{
    Task<UserDetailsModel> GetUserByIdAsync(int id);
    Task<UserDetailsModel> GetUserByEmailAsync(string email);
    Task<IEnumerable<UserListModel>> GetUsersAsync();
    Task<int> CreateUserAsync(CreateUserModel model);
    Task UpdateUserAsync(UpdateUserModel model);
    Task DeleteUserAsync(int id);
}
