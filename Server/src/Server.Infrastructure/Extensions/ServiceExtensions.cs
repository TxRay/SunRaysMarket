using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stripe;
using SunRaysMarket.Server.Application.Repositories;
using SunRaysMarket.Server.Application.UnitOfWork;
using SunRaysMarket.Server.Infrastructure.Data;
using SunRaysMarket.Server.Infrastructure.Data.PersistenceModels;
using SunRaysMarket.Server.Infrastructure.Repositories;
using SunRaysMarket.Server.Infrastructure.Seeding;
using SunRaysMarket.Server.Infrastructure.Services;
using SunRaysMarket.Server.Infrastructure.UnitOfWorkImplementation;
using SunRaysMarket.Shared.Services.Interfaces;
using TransactionRepository = SunRaysMarket.Server.Infrastructure.Repositories.TransactionRepository;

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
        services.AddScoped<IAddressRepository, AddressRepository>();
        services.AddScoped<ICartRepository, CartRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        services.AddScoped<IImageRepository, ImageRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
        services.AddScoped<ITimeSlotRepository, TimeSlotRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<IUnitOfMeasureRepository, UnitOfMeasureRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

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
        var appinfo = new AppInfo { Name = "Sun Rays Market Ecommerce", Version = "0.0.1", };

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
                httpClient: clientFactory?.CreateClient("Stripe"),
                maxNetworkRetries: StripeConfiguration.MaxNetworkRetries,
                appInfo: appinfo,
                enableTelemetry: StripeConfiguration.EnableTelemetry
            );

            return new StripeClient(apiKey: StripeConfiguration.ApiKey, httpClient: httpClient);
        });

        services.AddTransient<IPaymentService, StripePaymentService>();

        return services;
    }
}
