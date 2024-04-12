using Microsoft.Extensions.DependencyInjection;

namespace SunRaysMarket.Server.Infrastructure.Seeding;

internal interface ISeederFactory : IDisposable
{
    ISeeder CreateSeeder<TSeeder>()
        where TSeeder : ISeeder;

    ISeeder? CreateSeeder(Type seederType);

    public static ISeederFactory CreateSeederFactory(IServiceProvider serviceProvider)
    {
        return ActivatorUtilities.CreateInstance<SeederFactory>(serviceProvider);
    }
}