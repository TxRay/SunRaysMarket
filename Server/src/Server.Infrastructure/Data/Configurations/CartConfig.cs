using SunRaysMarket.Server.Infrastructure.Data.Configurations.Base;
using SunRaysMarket.Server.Infrastructure.Data.PersistenceModels;

namespace SunRaysMarket.Server.Infrastructure.Data.Configurations;

internal class CartConfig : TimeStampConfigurationBase<PersistenceModels.Cart>
{
    public override void Configure(EntityTypeBuilder<PersistenceModels.Cart> builder)
    {
        base.Configure(builder);

        builder.HasIndex(cart => cart.CustomerId).IsUnique();

        builder
            .HasOne(cart => cart.Customer)
            .WithOne(customer => customer.Cart)
            .HasForeignKey<PersistenceModels.Customer>(customer => customer.CartId);
    }
}
