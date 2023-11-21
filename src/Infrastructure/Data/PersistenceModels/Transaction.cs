using Infrastructure.Data.PersistenceModels.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.PersistenceModels;

internal class Transaction : TimeStampBaseModel
{
    public int Code { get; set; }
    public int CustomerId { get; set; }
    public int OrderId { get; set; }
    public int Status { get; set; }
    public string PaymentMethod { get; set; } = null!;
    public float AmountPaid { get; set; }

    public Customer? Customer { get; set; }
    public Order? Order { get; set; }
}
