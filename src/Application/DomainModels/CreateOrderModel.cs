using Application.Enums;

namespace Application.DomainModels;

public class CreateOrderModel
{
    public int CustomerId { get; init; }
    public int StoreId { get; init; }
    public int TimeSlotId { get; init; }
    public OrderType OrderType { get; init; }
    public OrderStatus Status { get; init; } = OrderStatus.Received;
}
