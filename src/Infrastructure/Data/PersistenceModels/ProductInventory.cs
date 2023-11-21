using Infrastructure.Data.PersistenceModels.Base;

namespace Infrastructure.Data.PersistenceModels;

internal class ProductInventory : TimeStampBaseModel
{
    public int ProductId { get; set; }
    public int StoreId { get; set; }
    public string Sku { get; set; } = default!;
    public string Barcode { get; set; } = default!;
    public string? Plu { get; set; }
    public int Quantity { get; set; }

    public Product? Product { get; set; }
    public Store? Store { get; set; }
}
