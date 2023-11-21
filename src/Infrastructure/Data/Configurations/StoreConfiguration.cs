using Infrastructure.Data.Configurations.Base;
using Infrastructure.Data.PersistenceModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

internal class StoreConfiguration : TimeStampBaseConfiguration<Store>
{
    public override void Configure(EntityTypeBuilder<Store> builder)
    {
        base.Configure(builder);

        builder.Property(store => store.LocationName).IsRequired().HasMaxLength(50);

        builder.Property(store => store.PhoneNumber).IsRequired().HasMaxLength(15);

        builder.Property(store => store.EmailAddress).IsRequired().HasMaxLength(50);

        builder.Property(store => store.ManagerName).IsRequired().HasMaxLength(50);

        builder
            .HasOne(store => store.Address)
            .WithOne()
            .HasForeignKey<Store>()
            .OnDelete(DeleteBehavior.Restrict);
    }
}
