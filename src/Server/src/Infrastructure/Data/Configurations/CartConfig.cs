using SunRaysMarket.Server.Infrastructure.Data.Configurations.Base;

namespace SunRaysMarket.Server.Infrastructure.Data.Configurations;

internal class CartConfig : TimeStampConfigurationBase<Cart>
{
    public override void Configure(EntityTypeBuilder<Cart> builder)
    {
        base.Configure(builder);

        builder.HasIndex(cart => cart.CustomerId).IsUnique();

        builder
            .HasOne(cart => cart.Customer)
            .WithOne(customer => customer.Cart)
            .HasForeignKey<Customer>(customer => customer.CartId);
    }
}
