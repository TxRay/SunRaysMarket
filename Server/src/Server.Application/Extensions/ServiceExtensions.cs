using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SunRaysMarket.Server.Application.Auth;
using SunRaysMarket.Server.Application.Services;
using SunRaysMarket.Shared.Services.Interfaces;

namespace SunRaysMarket.Server.Application.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddServerApplicationAssembly(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<SignUpService>();
        //services.AddAuthServices();
        //services.AddCustomerServices();
        services.AddScoped<ICartService, CartService>();
        services.AddScoped<IAddressService, AddressService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<ICheckoutService, CheckoutService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<ITransactionService, TransactionService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICustomerAddressService, CustomerAddressService>();

        return services;
    }
}
