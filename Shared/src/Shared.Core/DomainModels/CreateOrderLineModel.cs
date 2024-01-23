namespace SunRaysMarket.Shared.Core.DomainModels;

public class CreateOrderLineModel
{
    public int OrderId { get; set; }
    public int ItemId { get; set; }
    public int? OrderSubstitutionId { get; set; }
    public int Quantity { get; set; }
    public float Price { get; set; }
    public float Discount { get; set; }
    public float TotalPrice { get; set; }
}