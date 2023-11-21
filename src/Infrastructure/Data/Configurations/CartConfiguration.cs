using Infrastructure.Data.Configurations.Base;
using Infrastructure.Data.PersistenceModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

internal class CartConfiguration : TimeStampBaseConfiguration<Cart>
{
    public override void Configure(EntityTypeBuilder<Cart> builder)
    {
        base.Configure(builder);

        builder.HasIndex(cart => cart.CustomerId).IsUnique();

        builder.Property(cart => cart.CustomerId).IsRequired();

        builder.Property(cart => cart.CustomerId).IsRequired();

        builder
            .HasOne(cart => cart.Customer)
            .WithOne(customer => customer.Cart)
            .HasForeignKey<Customer>(customer => customer.CartId);
    }
}
