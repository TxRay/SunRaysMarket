using SunRaysMarket.Server.Infrastructure.Data.PersistenceModels.Base;

namespace SunRaysMarket.Server.Infrastructure.Data.PersistenceModels;

internal class Product : TimeStampModelBase
{
    public string Name { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string PhotoUrl { get; set; } = default!;
    public float Price { get; set; }
    public float DiscountPercent { get; set; }
    public int ProductTypeId { get; set; }
    public float Measure { get; set; }
    public int UnitOfMeasureId { get; set; }

    public ProductType? ProductType { get; set; }
    public UnitOfMeasure? UnitOfMeasure { get; set; }
    public virtual ICollection<List> Lists { get; set; } = new List<List>();

    public virtual ICollection<ProductInventory> InventoryItems { get; set; } =
        new List<ProductInventory>();
}
