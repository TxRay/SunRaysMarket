using Infrastructure.Data.PersistenceModels.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.PersistenceModels;

internal class Transaction : TimeStampBaseModel
{
    public int OrderId { get; set; }

    public int BillingAddressId { get; set; }
    public long TransactionNumber { get; set; }

    public string ChargeNumber { get; set; }
    public int Status { get; set; }
    public string PaymentMethod { get; set; } = null!;
    public float AmountPaid { get; set; }

    public Address? BillingAddress { get; set; }
    public Order? Order { get; set; }
}
