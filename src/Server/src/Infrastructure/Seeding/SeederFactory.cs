using Microsoft.Extensions.DependencyInjection;

namespace SunRaysMarket.Server.Infrastructure.Seeding;

internal class SeederFactory : ISeederFactory
{
    private readonly IServiceScope _serviceScope;

    public SeederFactory(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScope = serviceScopeFactory.CreateScope();
    }

    public ISeeder CreateSeeder<TSeeder>()
        where TSeeder : ISeeder
    {
        return ActivatorUtilities.CreateInstance<TSeeder>(_serviceScope.ServiceProvider);
    }

    public ISeeder CreateSeeder(Type seederType)
    {
        if (!seederType.IsAssignableTo(typeof(ISeeder)))
            throw new InvalidOperationException(
                $"The type '{seederType.FullName}' is not assignable to 'ISeeder'."
            );

        return (ISeeder)ActivatorUtilities.CreateInstance(_serviceScope.ServiceProvider, seederType)
            ?? throw new Exception("");
    }

    public void Dispose()
    {
        _serviceScope.Dispose();
    }
}
