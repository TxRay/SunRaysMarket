using Infrastructure.Data.Configurations.Base;
using Infrastructure.Data.PersistenceModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

internal class CartItemConfiguration : TimeStampBaseConfiguration<CartItem>
{
    public override void Configure(EntityTypeBuilder<CartItem> builder)
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
