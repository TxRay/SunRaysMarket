using SunRaysMarket.Server.Infrastructure.Data.Configurations.Base;

namespace SunRaysMarket.Server.Infrastructure.Data.Configurations;

internal class AddressConfig : ConfigurationBase<Address>
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