using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SunRaysMarket.Server.Application.Configuration;

namespace SunRaysMarket.Server.Infrastructure.Seeding;

internal class SuperAdminSeeder : ISeeder
{
    private readonly SuperAdminConfig _superAdminConfig;
    private readonly ILogger<SuperAdminSeeder> _logger;
    private readonly UserManager<User> _userManager;

    public SuperAdminSeeder(
        IConfiguration configuration,
        ILogger<SuperAdminSeeder> logger,
        UserManager<User> userManager
    )
    {
        _superAdminConfig = configuration
            .GetSection(SuperAdminConfig.GroupName)
            .Get<SuperAdminConfig>()!;
        ;
        _logger = logger;
        _userManager = userManager;
    }

    public async Task SeedAsync()
    {
        
        var superAdminUser = new User
        {
            UserName = _superAdminConfig.Email,
            Email = _superAdminConfig.Email,
            EmailConfirmed = false,
            FirstName = _superAdminConfig.FirstName,
            LastName = _superAdminConfig.LastName
        };

        var result = await _userManager.CreateAsync(superAdminUser, _superAdminConfig.Password);

        if (!result.Succeeded)
            throw new Exception("SuperAdmin user creation failed");

        var roleResult = await _userManager.AddToRoleAsync(superAdminUser, "SuperAdmin");

        if (!roleResult.Succeeded)
            throw new Exception("SuperAdmin role assignment failed");
    }

    public bool ShouldSeed() => _userManager.FindByEmailAsync(_superAdminConfig.Email).Result is not null;
}