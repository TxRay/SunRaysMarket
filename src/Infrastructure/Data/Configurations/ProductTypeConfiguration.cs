using Infrastructure.Data.Configurations.Base;
using Infrastructure.Data.PersistenceModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

internal class ProductTypeConfiguration : TimeStampBaseConfiguration<ProductType>
{
    public override void Configure(EntityTypeBuilder<ProductType> builder)
    {
        base.Configure(builder);

        builder.HasIndex(productType => productType.Name).IsUnique();

        builder.HasIndex(productType => productType.Slug).IsUnique();

        builder.Property(productType => productType.Name).IsRequired().HasMaxLength(50);

        builder.Property(productType => productType.Slug).IsRequired().HasMaxLength(50);

        builder.Property(productType => productType.Description).IsRequired().HasColumnType("text");

        builder
            .HasOne(productType => productType.Department)
            .WithMany(department => department.ProductTypes)
            .HasForeignKey(productType => productType.DepartmentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
