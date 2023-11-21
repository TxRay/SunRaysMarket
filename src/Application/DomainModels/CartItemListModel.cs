using Application.Structs;

namespace Application.DomainModels;

public class CartItemListModel
{
    public int CartId { get; init; }
    public int ProductId { get; init; }
    public string ProductName { get; init; } = null!;
    public string ProductSlug { get; init; } = null!;
    public int Quantity { get; init; }
    public float Price { get; init; }
}
