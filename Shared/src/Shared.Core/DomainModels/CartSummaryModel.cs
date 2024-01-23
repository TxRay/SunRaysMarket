namespace SunRaysMarket.Shared.Core.DomainModels;

public class CartSummaryModel
{
    public string Subtotal { get; init; } = null!;
    public string Discount { get; init; } = null!;
    public string Tax { get; init; } = null!;
    public string TotalPrice { get; init; } = null!;
}
