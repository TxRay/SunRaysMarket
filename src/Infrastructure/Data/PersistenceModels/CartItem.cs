using Infrastructure.Data.PersistenceModels.Base;

namespace Infrastructure.Data.PersistenceModels;

internal class CartItem : TimeStampBaseModel
{
    public int CartId { get; init; }
    public int ProductId { get; init; }
    public int Quantity { get; init; }

    public Cart? Cart { get; init; }
    public Product? Product { get; init; }
}
