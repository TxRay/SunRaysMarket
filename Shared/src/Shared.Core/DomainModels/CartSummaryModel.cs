using SunRaysMarket.Shared.Core.Structs;

namespace SunRaysMarket.Shared.Core.DomainModels;

public class CartSummaryModel
{
    public Price Subtotal { get; init; } 
    public Price Discount { get; init; }
    public Price Tax { get; init; }
    public Price TotalPrice { get; init; }
}
