using Microsoft.Extensions.DependencyInjection;
using SunRaysMarket.Client.Components.Modals;
using SunRaysMarket.Client.Components.Stores;

namespace SunRaysMarket.Client.Components.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddClientComponentServices(this IServiceCollection services)
    {
        services.AddSingleton<IModalController, ModalController>();
        services.AddSingleton<IStore, BrowserSessionStore>();

        return services;
    }
}