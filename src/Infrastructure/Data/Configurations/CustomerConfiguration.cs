using Infrastructure.Data.Configurations.Base;
using Infrastructure.Data.PersistenceModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

internal class CustomerConfiguration : BaseConfiguration<Customer>
{
    public override void Configure(EntityTypeBuilder<Customer> builder)
    {
        base.Configure(builder);

        builder.HasIndex(customer => customer.UserId).IsUnique();

        builder.HasIndex(customer => customer.CartId).IsUnique();

        builder.Property(customer => customer.UserId).IsRequired();

        builder
            .HasOne(customer => customer.Cart)
            .WithOne(cart => cart.Customer)
            .HasForeignKey<Customer>(customer => customer.CartId);

        builder
            .HasMany(customer => customer.Addresses)
            .WithMany()
            .UsingEntity<CustomerAddress>(
                caBuilder =>
                {
                    caBuilder.HasIndex(customerAddress => new { customerAddress.CustomerId, customerAddress.AddressId })
                        .IsUnique();

                    caBuilder.HasOne(customerAddress => customerAddress.Customer)
                        .WithMany()
                        .HasForeignKey(ca => ca.CustomerId);

                    caBuilder
                        .HasOne(customerAddress => customerAddress.Address)
                        .WithMany()
                        .HasForeignKey(customerAddress => customerAddress.AddressId);
                }
            );
    }
}