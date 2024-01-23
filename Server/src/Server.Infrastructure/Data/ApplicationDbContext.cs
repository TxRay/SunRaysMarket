using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SunRaysMarket.Server.Infrastructure.Data.PersistenceModels;

namespace SunRaysMarket.Server.Infrastructure.Data;

internal class ApplicationDbContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public ApplicationDbContext() { }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Address> Addresses { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<CustomerAddress> CustomerAddresses { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<List> Lists { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductType> ProductTypes { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderLine> OrderLine { get; set; }
    public DbSet<Product> Product { get; set; }
    public DbSet<Store> Stores { get; set; }
    public DbSet<TimeSlot> TimeSlots { get; set; }
    public DbSet<TimeSlotDefinition> TimeSlotDefinitions { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<UnitOfMeasure> UnitsOfMeasure { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // Identity Entity Model configuration
        base.OnModelCreating(builder);

        builder.Entity<User>(entity =>
        {
            entity.ToTable("Users");
        });

        builder.Entity<IdentityRole<int>>(entity =>
        {
            entity.ToTable("Roles");
        });

        builder.Entity<IdentityUserRole<int>>(entity =>
        {
            entity.ToTable("UserRoles");
        });

        builder.Entity<IdentityUserClaim<int>>(entity =>
        {
            entity.ToTable("UserClaims");
        });

        builder.Entity<IdentityUserLogin<int>>(entity =>
        {
            entity.ToTable("UserLogins");
        });

        builder.Entity<IdentityRoleClaim<int>>(entity =>
        {
            entity.ToTable("RoleClaims");
        });

        builder.Entity<IdentityUserToken<int>>(entity =>
        {
            entity.ToTable("UserTokens");
        });

        // Application Entity Model configuration
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        // Generate sequences
        builder.HasSequence<long>("OrderNumbers").StartsAt(1000000000).IncrementsBy(1);
        builder.HasSequence<long>("TransactionNumbers").StartsAt(1000000000).IncrementsBy(1);
    }

    /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(
            "Host=localhost; Database=srm_db;  User Id=srm_user; Password=Pass@123!"
        );
    }*/
}
