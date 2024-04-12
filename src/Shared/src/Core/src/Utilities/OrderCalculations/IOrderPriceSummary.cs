namespace SunRaysMarket.Shared.Core.Utilities.OrderCalculations;

public interface IOrderPriceSummary
{
    float Subtotal { get; set; }
    float Discount { get; set; }
    float Tax { get; set; }
    float Total { get; set; }
}