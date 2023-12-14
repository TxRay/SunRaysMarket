using Application.Enums;
using Application.Utilities.OrderCalculations;
using Infrastructure.Data.PersistenceModels.Base;

namespace Infrastructure.Data.PersistenceModels;

internal class Order : TimeStampBaseModel, IOrderPriceSummary
{
    public int CustomerId { get; set; }
    public int StoreId { get; set; }

    public int? DeliveryAddressId { get; set; }

    public int TimeSlotId { get; set; }

    public long OrderNumber { get; set; }

    public OrderType OrderType { get; set; }
    public float Subtotal { get; set; }
    public float Discount { get; set; }
    public float Tax { get; set; }
    public float Total { get; set; }

    public OrderStatus Status { get; set; }

    public Address? DeliveryAddress { get; set; }
    public Customer? Customer { get; set; }
    public Store? Store { get; set; }
    public TimeSlot? TimeSlot { get; set; }

    public virtual ICollection<OrderLine> OrderLines { get; set; } = new List<OrderLine>();
    public virtual ICollection<OrderSubstitution> OrderSubstitutions { get; set; } =
        new List<OrderSubstitution>();
    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
