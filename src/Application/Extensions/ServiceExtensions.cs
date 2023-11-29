using Application.Auth;
using Application.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<SignUpService>();
        services.AddAuthServices();
        services.AddCustomerServices();
        
        return services;
    }
    
    private static IServiceCollection AddAuthServices(this IServiceCollection services)
    {
        services.AddScoped<ILoginService, LoginService>();
        services.AddScoped<ISignUpService, SignUpService>();
        services.AddScoped<IUserService, UserService>();
        
        
        return services;
    }
    
    private static IServiceCollection AddCustomerServices(this IServiceCollection services)
    {
        services.AddScoped<ICustomerService, CustomerService>();
        
        return services;
    }
}