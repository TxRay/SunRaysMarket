using SunRaysMarket.Server.Infrastructure.Data.Configurations.Base;

namespace SunRaysMarket.Server.Infrastructure.Data.Configurations;

internal class UnitOfMeasureConfig : ConfigurationBase<UnitOfMeasure>
{
    public override void Configure(EntityTypeBuilder<UnitOfMeasure> builder)
    {
        base.Configure(builder);

        builder.ToTable("UnitsOfMeasure");

        builder.HasIndex(u => u.Name).IsUnique();

        builder.HasIndex(u => u.Symbol).IsUnique();

        builder.Property(u => u.Name).HasMaxLength(50).IsRequired();

        builder.Property(u => u.Symbol).HasMaxLength(10).IsRequired();
    }
}