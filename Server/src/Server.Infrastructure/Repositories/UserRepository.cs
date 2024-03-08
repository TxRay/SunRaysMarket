using SunRaysMarket.Server.Application.Enums;
using SunRaysMarket.Server.Application.Repositories;

namespace SunRaysMarket.Server.Infrastructure.Repositories;

internal class UserRepository(
    RoleManager<IdentityRole<int>> roleManager,
    UserManager<User> userManager,
    SignInManager<User> signInManager
) : IUserRepository
{
    private readonly RoleManager<IdentityRole<int>> _roleManager = roleManager;
    private User? _persistedUser;

    public async Task<UserDetailsModel?> GetUserByIdAsync(int id)
    {
        return await userManager
            .Users
            .Where(u => u.Id == id)
            .Select(
                u =>
                    new UserDetailsModel
                    {
                        Id = u.Id,
                        Email = u.Email!,
                        FirstName = u.FirstName,
                        LastName = u.LastName
                    }
            )
            .FirstOrDefaultAsync();
    }

    public async Task<UserDetailsModel?> GetUserByEmailAsync(string email)
    {
        return await userManager
            .Users
            .Where(u => u.Email == email)
            .Select(
                u =>
                    new UserDetailsModel
                    {
                        Id = u.Id,
                        Email = u.Email!,
                        FirstName = u.FirstName,
                        LastName = u.LastName
                    }
            )
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<UserListModel>> GetUsersAsync()
    {
        return await userManager
            .Users
            .Select(
                u =>
                    new UserListModel
                    {
                        Id = u.Id,
                        Email = u.Email!,
                        FirstName = u.FirstName,
                        LastName = u.LastName
                    }
            )
            .ToListAsync();
    }

    public async Task<UserDetailsModel?> CreateUserAsync(SignUpModel model)
    {
        var result = await userManager.CreateAsync(
            new User
            {
                Email = model.Email,
                UserName = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
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

    public async Task<bool> HasRolesAsync(int userId, Role[] roles)
    {
        var roleStrings = roles.Select(r => r.ToString()).ToArray();
        var user = _persistedUser ?? await userManager.FindByIdAsync(userId.ToString());

        if (user is null)
            return false;

        var userRoles = await userManager.GetRolesAsync(user);

        return roleStrings.All(rs => userRoles.Contains(rs));
    }

    public async Task<bool> HasRoleAsync(int userId, Role role)
    {
        return await HasRolesAsync(userId, [role]);
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
