using Application.DomainModels;
using Application.Repositories;

namespace Infrastructure.Repositories;

internal class UserRepository : IUserRepository
{
    public Task<UserDetailsModel> GetUserByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<UserDetailsModel> GetUserByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UserListModel>> GetUsersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<int> CreateUserAsync(CreateUserModel model)
    {
        throw new NotImplementedException();
    }

    public Task UpdateUserAsync(UpdateUserModel model)
    {
        throw new NotImplementedException();
    }

    public Task DeleteUserAsync(int id)
    {
        throw new NotImplementedException();
    }
}
