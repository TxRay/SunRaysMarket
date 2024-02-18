using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stripe;
using SunRaysMarket.Server.Application.UnitOfWork;
using SunRaysMarket.Server.Infrastructure.PaymentServices;
using SunRaysMarket.Server.Infrastructure.Seeding;
using SunRaysMarket.Server.Infrastructure.UnitOfWorkImplementation;
using SunRaysMarket.Shared.Extensions.Reflection;
using SunRaysMarket.Shared.Services.Interfaces;

namespace SunRaysMarket.Server.Infrastructure.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddServerInfrastructureAssembly(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddApplicationDbContext(configuration);
        services.AddAuthentication();
        services.AddRepositoryServices();
        services.AddUnitOfWorkServices();
        services.AddSeederServices();
        services.AddStripe(configuration);
        services.AddServiceImplementations();

        return services;
    }

    private static IServiceCollection AddApplicationDbContext(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddDbContextFactory<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(
                "Host=localhost; Database=srm_db;  User Id=srm_user; Password=Pass@123!"
            );
        });

        return services;
    }

    private static IServiceCollection AddAuthentication(this IServiceCollection services)
    {
        services
            .AddIdentity<User, IdentityRole<int>>(options =>
            {
                options.ClaimsIdentity.EmailClaimType = ClaimTypes.Email;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>();

        return services;
    }

    private static IServiceCollection AddRepositoryServices(this IServiceCollection services)
    {
        var interfaceNamespaceDescriptors = new[]
        {
            new NamespaceDescriptor(
                typeof(IUnitOfWork).Assembly,
                "SunRaysMarket.Server.Application.Repositories"
            )
        };
        services.AddInterfacesWithImplementationsFromLocalNamespace(
            interfaceNamespaceDescriptors,
            "SunRaysMarket.Server.Infrastructure.Repositories",
            ServiceLifetime.Scoped
        );

        return services;
    }

    private static IServiceCollection AddUnitOfWorkServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

    private static IServiceCollection AddSeederServices(this IServiceCollection services)
    {
        services.AddScoped<IDepartmentSeeder, DepartmentSeeder>();
        services.AddScoped<ISuperAdminSeeder, SuperAdminSeeder>();
        services.AddScoped<ITimeSlotDefinitionsSeeder, TimeSlotDefinitionsSeeder>();
        services.AddScoped<IUnitsOfMeasureSeeder, UnitsOfMeasureSeeder>();
        services.AddScoped<IUserRolesSeeder, UserRolesSeeder>();
        services.AddScoped<IStoreSeeder, StoreSeeder>();
        services.AddScoped<ITimeSlotSeeder, TimeSlotSeeder>();

        return services;
    }

    private static IServiceCollection AddStripe(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var appinfo = new AppInfo { Name = "Sun Rays Market Ecommerce", Version = "0.0.1" };

        StripeConfiguration.ApiKey =
            configuration["Stripe:SecretKey"]
            ?? throw new InvalidOperationException(
                "Stripe:SecretKey is not set in appsettings.json"
            );
        StripeConfiguration.AppInfo = appinfo;

        services.AddHttpClient("Stripe");
        services.AddTransient<IStripeClient, StripeClient>(provider =>
        {
            var clientFactory = provider.GetService<IHttpClientFactory>();
            var httpClient = new SystemNetHttpClient(
                clientFactory?.CreateClient("Stripe"),
                StripeConfiguration.MaxNetworkRetries,
                appinfo,
                StripeConfiguration.EnableTelemetry
            );

            return new StripeClient(StripeConfiguration.ApiKey, httpClient: httpClient);
        });

        services.AddTransient<IPaymentService, StripePaymentService>();

        return services;
    }

    private static IServiceCollection AddServiceImplementations(this IServiceCollection services)
    {
        var interfaceNamespaceDescriptors = new[]
        {
            new NamespaceDescriptor(
                typeof(IUnitOfWork).Assembly,
                "SunRaysMarket.Server.Application.Services"
            )
        };

        services.AddInterfacesWithImplementationsFromLocalNamespace(
            interfaceNamespaceDescriptors,
            "SunRaysMarket.Server.Infrastructure.ServicesImpl"
        );

        return services;
    }
}