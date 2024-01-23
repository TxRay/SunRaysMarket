using SunRaysMarket.Shared.Core.Enums;

namespace SunRaysMarket.Shared.Core.DomainModels;

public class CreateOrderModel
{
    public int CustomerId { get; init; }
    public int StoreId { get; init; }
    public int TimeSlotId { get; init; }

    public int? DeliveryAddressId { get; init; }
    public OrderType OrderType { get; init; }
    public OrderStatus Status { get; init; } = OrderStatus.Received;
}