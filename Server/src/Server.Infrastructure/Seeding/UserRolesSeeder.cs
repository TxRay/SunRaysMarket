namespace SunRaysMarket.Server.Infrastructure.Seeding;

internal class UserRolesSeeder : ISeeder
{
    private readonly ApplicationDbContext _dbContext;
    private readonly RoleManager<IdentityRole<int>> _roleManager;

    public UserRolesSeeder(
        ApplicationDbContext dbContext,
        RoleManager<IdentityRole<int>> roleManager
    )
    {
        _dbContext = dbContext;
        _roleManager = roleManager;
    }

    public async Task SeedAsync()
    {
        if (_dbContext.Roles.Any())
            return;

        var roleNames = new List<string>
        {
            "SuperAdmin",
            "Admin",
            "Customer",
            "Employee",
            "Manager"
        };

        foreach (
            var role in roleNames.Select(
                roleName =>
                    new IdentityRole<int>
                    {
                        Name = roleName,
                        NormalizedName = roleName.ToUpper(),
                        ConcurrencyStamp = Guid.NewGuid().ToString()
                    }
            )
        )
            await _roleManager.CreateAsync(role);
    }

    public bool ShouldSeed() => !_dbContext.Roles.Any();
}