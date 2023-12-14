using Application.Utilities.OrderCalculations;
using Infrastructure.Data.PersistenceModels.Base;

namespace Infrastructure.Data.PersistenceModels;

internal class OrderLine : TimeStampBaseModel, IOrderItemAmounts
{
    public int OrderId { get; set; }
    public int ItemId { get; set; }
    public int? OrderSubstitutionId { get; set; }
    public int Quantity { get; set; }

    public float Price { get; set; }
    public float Discount { get; set; }
    public float TotalPrice { get; set; }

    public Order? Order { get; set; }
    public OrderSubstitution? OrderSubstitution { get; set; }
    public Product? Item { get; set; }
}
