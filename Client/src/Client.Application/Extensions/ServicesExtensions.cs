using Microsoft.Extensions.DependencyInjection;
using SunRaysMarket.Client.Application.ProxyServices;
using SunRaysMarket.Client.Application.State;
using SunRaysMarket.Shared.Services.Interfaces;

namespace SunRaysMarket.Client.Application.Extensions;

public static class ServicesExtensions
{

    public static IServiceCollection AddClientOnlyApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IAddressService, AddressProxyService>();
        services.AddScoped<ICartService, CartProxyService>();
        services.AddScoped<ICheckoutService, CheckoutProxyService>();
        services.AddScoped<IPaymentService, PaymentProxyService>();
        services.AddScoped<IProductService, ProductProxyService>();
        services.AddScoped<ICustomerAddressService, CustomerAddressProxyService>();
        services.AddSingleton<ProductModalState>();
        
        return services;
    }
}
