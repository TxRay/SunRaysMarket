using Application.DomainModels.BaseModels;
using Application.Enums;
using Application.Structs;

namespace Application.DomainModels;

public class OrderListModel : BaseDomainModel
{
    public int CustomerId { get; init; }
    public string CustomerName { get; init; } = default!;
    public int StoreId { get; init; }

    public int? DeliveryAddressId { get; init; }
    public string StoreName { get; init; } = default!;
    public TimeSlotStruct TimeSlot { get; init; } = default!;
    public float Total { get; init; }
    public OrderStatus Status { get; init; }
}
