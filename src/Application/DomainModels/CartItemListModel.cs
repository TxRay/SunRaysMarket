using Application.BaseModels;
using Application.Structs;

namespace Application.DomainModels;

public class CartItemListModel : TimeStampBaseDomainModel
{
    public int CartId { get; init; }
    public int ProductId { get; init; }
    public string ProductName { get; init; } = null!;
    public string ProductSlug { get; init; } = null!;
    public string ProductPhotoUrl { get; init; } = null!;
    public int Quantity { get; init; }
    public float RegularPrice { private get; init; }
    public float DiscountDecimal { private get; init; }
    
    public float ProductPrice => RegularPrice - (RegularPrice * DiscountDecimal);
    
    
}
