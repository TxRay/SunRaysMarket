using SunRaysMarket.Server.Infrastructure.Data.Configurations.Base;

namespace SunRaysMarket.Server.Infrastructure.Data.Configurations;

internal class ProductConfig : TimeStampConfigurationBase<Product>
{
    public override void Configure(EntityTypeBuilder<Product> builder)
    {
        base.Configure(builder);

        builder.Property(product => product.Name).HasMaxLength(100).IsRequired();

        builder.Property(product => product.Slug).HasMaxLength(100).IsRequired();

        builder.Property(product => product.Description).HasColumnType("text").IsRequired();

        builder.Property(product => product.PhotoUrl).HasMaxLength(255).IsRequired();

        builder.Property(product => product.Price).HasColumnType("decimal(5,2)").IsRequired();

        builder
            .Property(product => product.DiscountPercent)
            .HasColumnType("decimal(3,2)")
            .IsRequired();

        builder.Property(product => product.ProductTypeId).IsRequired();
        builder.Property(product => product.Measure).HasColumnType("decimal(5,2)").IsRequired();
        builder.Property(product => product.UnitOfMeasureId).IsRequired();

        builder
            .HasOne(product => product.ProductType)
            .WithMany(productType => productType.Products)
            .HasForeignKey(product => product.ProductTypeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(product => product.UnitOfMeasure)
            .WithMany(unitOfMeasure => unitOfMeasure.Products)
            .HasForeignKey(product => product.UnitOfMeasureId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
