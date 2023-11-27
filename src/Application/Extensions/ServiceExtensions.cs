using Application.Auth;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<SignUpService>();
        services.AddAuthServices();
        
        return services;
    }
    
    private static IServiceCollection AddAuthServices(this IServiceCollection services)
    {
        services.AddScoped<ILoginService, LoginService>();
        services.AddScoped<ISignUpService, SignUpService>();
        services.AddScoped<IUserService, UserService>();
        
        return services;
    }
}