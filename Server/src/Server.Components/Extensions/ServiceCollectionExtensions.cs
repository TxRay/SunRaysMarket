using Microsoft.Extensions.DependencyInjection;
using SunRaysMarket.Server.Components.Base;

namespace SunRaysMarket.Server.Components.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServerComponentsAssembly(this IServiceCollection services)
    {
        services.AddScoped<PageStateProvider>();
        
        return services;
    }
}