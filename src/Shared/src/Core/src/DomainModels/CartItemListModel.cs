using System.Text.Json.Serialization;
using SunRaysMarket.Shared.Core.DomainModels.BaseModels;

namespace SunRaysMarket.Shared.Core.DomainModels;

public class CartItemListModel : TimeStampBaseDomainModel
{
    public int CartId { get; init; }
    public int ProductId { get; init; }
    public string ProductName { get; init; } = null!;
    public string ProductSlug { get; init; } = null!;
    public string ProductPhotoUrl { get; init; } = null!;
    public int Quantity { get; init; }
    public float RegularPrice { get; init; }

    [JsonInclude] public float DiscountDecimal { private get; init; }

    [JsonIgnore] public float Discount => RegularPrice * DiscountDecimal;

    [JsonIgnore] public float ProductPrice => RegularPrice - Discount;
}