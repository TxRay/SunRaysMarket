using Infrastructure.Data.Configurations.Base;
using Infrastructure.Data.PersistenceModels;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Configurations;

internal class UnitOfMeasureConfiguration : BaseConfiguration<UnitOfMeasure>
{
    public override void Configure(
        Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<UnitOfMeasure> builder
    )
    {
        base.Configure(builder);

        builder.ToTable("UnitsOfMeasure");

        builder.HasIndex(u => u.Name).IsUnique();

        builder.HasIndex(u => u.Symbol).IsUnique();

        builder.Property(u => u.Name).HasMaxLength(50).IsRequired();

        builder.Property(u => u.Symbol).HasMaxLength(10).IsRequired();
    }
}
