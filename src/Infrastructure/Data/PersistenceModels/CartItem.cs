using Infrastructure.Data.PersistenceModels.Base;

namespace Infrastructure.Data.PersistenceModels;

internal class CartItem : TimeStampBaseModel
{
    public int CartId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }

    public Cart? Cart { get; set; }
    public Product? Product { get; set; }
}
