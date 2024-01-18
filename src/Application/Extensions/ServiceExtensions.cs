using Application.Auth;
using Application.Repositories;
using Application.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
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
