namespace Application.DomainModels;

public class OrderLineListModel
{
    public int OrderId { get; init; }
    public int ItemId { get; init; }
    public int Quantity { get; init; }
    public float Price { get; init; }
    public float Discount { get; init; }
    public float TotalPrice { get; init; }
}
