using Infrastructure.Data;
using Infrastructure.Data.PersistenceModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Seeding;

internal interface ISuperAdminSeeder : ISeeder { }

internal class SuperAdminSeeder : ISuperAdminSeeder
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<SuperAdminSeeder> _logger;
    private readonly UserManager<User> _userManager;

    public SuperAdminSeeder(
        IConfiguration configuration,
        ILogger<SuperAdminSeeder> logger,
        UserManager<User> userManager
    )
    {
        _configuration = configuration;
        _logger = logger;
        _userManager = userManager;
    }

    public async Task SeedAsync()
    {
        var superAdminEmail =
            _configuration["SuperAdmin:Email"] ?? throw new Exception("SuperAdmin email not found");
        var superAdminPassword =
            _configuration["SuperAdmin:Password"]
            ?? throw new Exception("SuperAdmin password not found");
        var superAdminFirstName =
            _configuration["SuperAdmin:FirstName"]
            ?? throw new Exception("SuperAdmin first name not found");
        var superAdminLastName =
            _configuration["SuperAdmin:LastName"]
            ?? throw new Exception("SuperAdmin last name not found");

        var superAdminUser = await _userManager.FindByEmailAsync(superAdminEmail);

        if (superAdminUser is not null)
            return;

        superAdminUser = new User
        {
            UserName = superAdminEmail,
            Email = superAdminEmail,
            EmailConfirmed = false,
            FirstName = superAdminFirstName,
            LastName = superAdminLastName,
        };

        var result = await _userManager.CreateAsync(superAdminUser, superAdminPassword);

        if (!result.Succeeded)
            throw new Exception("SuperAdmin user creation failed");

        var roleResult = await _userManager.AddToRoleAsync(superAdminUser, "SuperAdmin");

        if (!roleResult.Succeeded)
            throw new Exception("SuperAdmin role assignment failed");
    }
}
