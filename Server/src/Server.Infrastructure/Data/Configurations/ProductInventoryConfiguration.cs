using SunRaysMarket.Server.Infrastructure.Data.Configurations.Base;
using SunRaysMarket.Server.Infrastructure.Data.PersistenceModels;

namespace SunRaysMarket.Server.Infrastructure.Data.Configurations;

internal class ProductInventoryConfiguration : TimeStampBaseConfiguration<ProductInventory>
{
    public override void Configure(EntityTypeBuilder<ProductInventory> builder)
    {
        base.Configure(builder);

        builder
            .HasIndex(
                productInventory => new { productInventory.ProductId, productInventory.StoreId }
            )
            .IsUnique();

        builder.Property(productInventory => productInventory.ProductId).IsRequired();
        builder.Property(productInventory => productInventory.StoreId).IsRequired();
        builder.Property(productInventory => productInventory.Sku).HasMaxLength(50).IsRequired();
        builder
            .Property(productInventory => productInventory.Barcode)
            .HasMaxLength(50)
            .IsRequired();
        builder.Property(productInventory => productInventory.Plu).HasMaxLength(50);
        builder.Property(productInventory => productInventory.Quantity).IsRequired();

        builder
            .HasOne(productInventory => productInventory.Product)
            .WithMany(product => product.InventoryItems)
            .HasForeignKey(productInventory => productInventory.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(productInventory => productInventory.Store)
            .WithMany(store => store.InventoryItems)
            .HasForeignKey(productInventory => productInventory.StoreId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
