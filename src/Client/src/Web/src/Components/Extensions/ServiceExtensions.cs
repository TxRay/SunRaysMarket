using SunRaysMarket.Client.Web.Components.Modals;
using SunRaysMarket.Client.Web.Components.Stores;

namespace SunRaysMarket.Client.Web.Components.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddClientComponentServices(this IServiceCollection services)
    {
        services.AddSingleton<IModalController, ModalController>();
        services.AddSingleton<IStore, BrowserSessionStore>();

        return services;
    }
}