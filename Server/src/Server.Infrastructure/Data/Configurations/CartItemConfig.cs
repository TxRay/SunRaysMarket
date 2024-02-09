using SunRaysMarket.Server.Infrastructure.Data.Configurations.Base;
using SunRaysMarket.Server.Infrastructure.Data.PersistenceModels;

namespace SunRaysMarket.Server.Infrastructure.Data.Configurations;

internal class CartItemConfig : TimeStampConfigurationBase<PersistenceModels.CartItem>
{
    public override void Configure(EntityTypeBuilder<PersistenceModels.CartItem> builder)
    {
        base.Configure(builder);

        builder.HasIndex(cartItem => new { cartItem.CartId, cartItem.ProductId }).IsUnique();

        builder.Property(cartItem => cartItem.CartId).IsRequired();

        builder.Property(cartItem => cartItem.ProductId).IsRequired();

        builder.Property(cartItem => cartItem.Quantity).IsRequired();

        builder
            .HasOne(cartItem => cartItem.Cart)
            .WithMany(cart => cart.CartItems)
            .HasForeignKey(cartItem => cartItem.CartId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(cartItem => cartItem.Product)
            .WithMany()
            .HasForeignKey(cartItem => cartItem.ProductId);
    }
}
