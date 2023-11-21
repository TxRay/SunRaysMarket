using Infrastructure.Seeding;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

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
        var timeSlotDefinitionsSeeder =
            scope.ServiceProvider.GetRequiredService<ITimeSlotDefinitionsSeeder>();
        var unitOfMeasureSeeder = scope.ServiceProvider.GetRequiredService<IUnitsOfMeasureSeeder>();
        var userRolesSeeder = scope.ServiceProvider.GetRequiredService<IUserRolesSeeder>();

        await departmentSeeder.SeedAsync();
        await userRolesSeeder.SeedAsync();
        await superAdminSeeder.SeedAsync();
        await timeSlotDefinitionsSeeder.SeedAsync();
        await unitOfMeasureSeeder.SeedAsync();
    }
}
