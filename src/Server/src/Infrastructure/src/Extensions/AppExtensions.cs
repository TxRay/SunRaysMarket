using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SunRaysMarket.Server.Infrastructure.Seeding;

namespace SunRaysMarket.Server.Infrastructure.Extensions;

public static class AppExtensions
{
    public static IApplicationBuilder SeedDatabase(this IApplicationBuilder app)
    {
        app.SeedAsync().Wait();

        return app;
    }

    public static IApplicationBuilder ApplyMigrations(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        dbContext.Database.Migrate();

        return app;
    }

    private static async Task SeedAsync(this IApplicationBuilder app)
    {
        using var seederFactory = ISeederFactory.CreateSeederFactory(app.ApplicationServices);

        var seederImplementations = Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Where(
                type =>
                    type
                        is
                        {
                            Namespace: "SunRaysMarket.Server.Infrastructure.Seeding",
                            IsAbstract: false
                        }
                    && type.IsAssignableTo(typeof(ISeeder))
            );

        foreach (var seederType in seederImplementations)
        {
            var seeder = seederFactory.CreateSeeder(seederType)!;

            if (seeder.ShouldSeed())
                await seeder.SeedAsync();
        }
    }
}