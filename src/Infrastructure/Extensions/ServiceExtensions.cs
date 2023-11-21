using Application.Repositories;
using Application.UnitOfWork;
using Infrastructure.Data;
using Infrastructure.Data.PersistenceModels;
using Infrastructure.Repositories;
using Infrastructure.Seeding;
using Infrastructure.UnitOfWorkImplementation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddApplicationDbContext(configuration);
        services.AddAuthentication();
        services.AddRepositoryServices();
        services.AddUnitOfWorkServices();
        services.AddSeederServices();

        return services;
    }

    private static IServiceCollection AddApplicationDbContext(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddDbContext<ApplicationDbContext>();

        return services;
    }

    private static IServiceCollection AddAuthentication(this IServiceCollection services)
    {
        services
            .AddIdentity<User, IdentityRole<int>>(options =>
            {
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

        return services;
    }
}
