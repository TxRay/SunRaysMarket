namespace SunRaysMarket.Shared.Core.Utilities.OrderCalculations;

public interface IOrderItemAmounts
{
    int Quantity { get; set; }
    public float Price { get; set; }
    public float Discount { get; set; }
    public float TotalPrice { get; set; }
}