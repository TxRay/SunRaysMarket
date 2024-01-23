using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SunRaysMarket.Server.Infrastructure.Seeding;

namespace SunRaysMarket.Server.Infrastructure.Extensions;

public static class AppExtensions
{
    public static IApplicationBuilder UseDbConfiguration(this IApplicationBuilder app)
    {
        app.SeedAsync().Wait();

        return app;
    }

    private static async Task SeedAsync(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var services = scope.ServiceProvider;

        var departmentSeeder = scope.ServiceProvider.GetRequiredService<IDepartmentSeeder>();
        var superAdminSeeder = scope.ServiceProvider.GetRequiredService<ISuperAdminSeeder>();
        var timeSlotDefinitionsSeeder = scope
            .ServiceProvider
            .GetRequiredService<ITimeSlotDefinitionsSeeder>();
        var unitOfMeasureSeeder = scope.ServiceProvider.GetRequiredService<IUnitsOfMeasureSeeder>();
        var userRolesSeeder = scope.ServiceProvider.GetRequiredService<IUserRolesSeeder>();
        var storeSeeder = scope.ServiceProvider.GetRequiredService<IStoreSeeder>();
        var timeSlotSeeder = scope.ServiceProvider.GetRequiredService<ITimeSlotSeeder>();

        await superAdminSeeder.SeedAsync();
        await departmentSeeder.SeedAsync();
        await userRolesSeeder.SeedAsync();
        await timeSlotDefinitionsSeeder.SeedAsync();
        await storeSeeder.SeedAsync();
        await timeSlotSeeder.SeedAsync();
        await unitOfMeasureSeeder.SeedAsync();
    }
}
