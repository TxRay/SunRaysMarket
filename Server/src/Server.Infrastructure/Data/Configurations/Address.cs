using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SunRaysMarket.Server.Infrastructure.Data.Configurations.Base;
using SunRaysMarket.Server.Infrastructure.Data.PersistenceModels;

namespace SunRaysMarket.Server.Infrastructure.Data.Configurations;

internal class Address : ConfigurationBase<PersistenceModels.Address>
{
    public override void Configure(EntityTypeBuilder<PersistenceModels.Address> builder)
    {
        base.Configure(builder);

        builder.Property(address => address.Street).HasMaxLength(2048).IsRequired();

        builder.Property(address => address.City).HasMaxLength(50).IsRequired();

        builder.Property(address => address.State).HasMaxLength(5).IsRequired();

        builder.Property(address => address.PostalCode).HasMaxLength(10).IsRequired();

        builder.Property(address => address.Country).HasMaxLength(30).IsRequired();
    }
}
