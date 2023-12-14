using Application.Auth;
using Application.DomainModels;
using Application.Repositories;
using Infrastructure.Data.PersistenceModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

internal class UserRepository(
    RoleManager<IdentityRole<int>> roleManager,
    UserManager<User> userManager,
    SignInManager<User> signInManager
) : IUserRepository
{
    private readonly RoleManager<IdentityRole<int>> _roleManager = roleManager;

    public async Task<UserDetailsModel?> GetUserByIdAsync(int id) =>
        await userManager
            .Users
            .Where(u => u.Id == id)
            .Select(
                u =>
                    new UserDetailsModel
                    {
                        Id = u.Id,
                        Email = u.Email!,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                    }
            )
            .FirstOrDefaultAsync();

    public async Task<UserDetailsModel?> GetUserByEmailAsync(string email) =>
        await userManager
            .Users
            .Where(u => u.Email == email)
            .Select(
                u =>
                    new UserDetailsModel
                    {
                        Id = u.Id,
                        Email = u.Email!,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                    }
            )
            .FirstOrDefaultAsync();

    public async Task<IEnumerable<UserListModel>> GetUsersAsync() =>
        await userManager
            .Users
            .Select(
                u =>
                    new UserListModel
                    {
                        Id = u.Id,
                        Email = u.Email!,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                    }
            )
            .ToListAsync();

    public async Task<UserDetailsModel?> CreateUserAsync(SignUpModel model)
    {
        var result = await userManager.CreateAsync(
            new User
            {
                Email = model.Email,
                UserName = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
            },
            model.Password
        );

        if (!result.Succeeded)
            return null;

        return await GetUserByEmailAsync(model.Email);
    }

    public async Task AddUserRolesAsync(int userId, IEnumerable<Role> roles)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());

        if (user is null)
            return;

        var roleNames = roles.Select(r => r.ToString());

        await userManager.AddToRolesAsync(user, roleNames);
    }

    public Task UpdateUserAsync(UpdateUserModel model)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteUserAsync(int id)
    {
        var user = await userManager.FindByIdAsync(id.ToString());

        if (user is not null)
            await userManager.DeleteAsync(user);
    }

    public async Task<UserDetailsModel?> AuthenticateAsync(LoginModel model)
    {
        var result = await signInManager.PasswordSignInAsync(
            model.Email,
            model.Password,
            false,
            false
        );

        if (!result.Succeeded)
            return null;

        return await GetUserByEmailAsync(model.Email);
    }

    public async Task LogoutAsync()
    {
        await signInManager.SignOutAsync();
    }
}
