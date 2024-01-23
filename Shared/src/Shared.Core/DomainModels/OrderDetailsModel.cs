using SunRaysMarket.Shared.Core.Enums;
using SunRaysMarket.Shared.Core.Structs;

namespace SunRaysMarket.Shared.Core.DomainModels;

public class OrderDetailsModel
{
    public int Id { get; init; }
    public int CustomerId { get; init; }
    public string CustomerName { get; init; } = default!;
    public long OrderNumber { get; init; }
    public int StoreId { get; init; }

    public int? DeliveryAddressId { get; init; }
    public string StoreName { get; init; } = default!;
    public TimeSlotStruct TimeSlot { get; init; }
    public float Subtotal { get; init; }
    public float Discount { get; init; }
    public float Tax { get; init; }
    public float Total { get; init; }
    public OrderStatus Status { get; init; }
}
