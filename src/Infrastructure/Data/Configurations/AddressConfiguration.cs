using Infrastructure.Data.Configurations.Base;
using Infrastructure.Data.PersistenceModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

internal class AddressConfiguration : BaseConfiguration<Address>
{
    public override void Configure(EntityTypeBuilder<Address> builder)
    {
        base.Configure(builder);

        builder.Property(address => address.Street).HasMaxLength(2048).IsRequired();

        builder.Property(address => address.City).HasMaxLength(50).IsRequired();

        builder.Property(address => address.State).HasMaxLength(5).IsRequired();

        builder.Property(address => address.PostalCode).HasMaxLength(10).IsRequired();

        builder.Property(address => address.Country).HasMaxLength(30).IsRequired();
    }
}
