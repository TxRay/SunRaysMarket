using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stripe;
using SunRaysMarket.Server.Application.UnitOfWork;
using SunRaysMarket.Server.Core.Services;
using SunRaysMarket.Server.Infrastructure.Configuration;
using SunRaysMarket.Server.Infrastructure.PaymentServices;
using SunRaysMarket.Server.Infrastructure.UnitOfWorkImplementation;
using SunRaysMarket.Shared.Core.Services;
using SunRaysMarket.Shared.Extensions.Reflection;

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
        services.AddStripe(configuration);
        services.AddServiceImplementations();
        services.AddDistributedMemoryCache();

        return services;
    }

    private static IServiceCollection AddApplicationDbContext(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddDbContextFactory<ApplicationDbContext>(options =>
        {
            var dbConnectionOptions =
                configuration
                    .GetSection(DbConnectionOptions.DbConnection)
                    .Get<DbConnectionOptions>() ?? new DbConnectionOptions();

            options.UseNpgsql(dbConnectionOptions.ToString());
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
        var interfaceNamespaceDescriptors = NamespaceDescriptor.FromTypes(typeof(IOrderService));

        services.AddInterfacesWithImplementationsFromLocalNamespace(
            interfaceNamespaceDescriptors,
            "SunRaysMarket.Client.Application.ProxyServicesImpl"
        );

        return services;
    }
}