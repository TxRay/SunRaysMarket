using Application.Enums;

namespace Application.DomainModels;

public class UpdateOrderModel
{
    public int TimeSlotId { get; init; }
    public float Subtotal { get; init; }
    public float Discount { get; init; }
    public float Tax { get; init; }
    public float Total { get; init; }
    public OrderStatus Status { get; init; }
}
