using Application.Services;
using WebClient.Components.Modals;
using WebClient.ProxyServices;
using WebClient.State;
using WebClient.Stores;

namespace WebClient.Extensions;

public static class ServicesExtensions
{
    public static IServiceCollection AddWebClientServices(this IServiceCollection services)
    {
        services.AddSingleton<IWizardModelStore, WizardModelStore>();
        services.AddSingleton<IStoreFactory, StoreFactory>();

        return services;
    }

    public static IServiceCollection AddWebClientOnlyServices(this IServiceCollection services)
    {
        services.AddScoped<IAddressService, AddressProxyService>();
        services.AddScoped<ICartService, CartProxyService>();
        services.AddScoped<ICheckoutService, CheckoutProxyService>();
        services.AddScoped<IPaymentService, PaymentProxyService>();
        services.AddScoped<IProductService, ProductProxyService>();
        services.AddScoped<ICustomerAddressService, CustomerAddressProxyService>();

        services.AddSingleton<ProductModalState>();
        services.AddSingleton<IModalController, ModalController>();
        services.AddSingleton<IStore, BrowserSessionStore>();

        return services;
    }
}
