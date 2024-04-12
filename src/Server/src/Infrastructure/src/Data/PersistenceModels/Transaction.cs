using SunRaysMarket.Server.Infrastructure.Data.PersistenceModels.Base;

namespace SunRaysMarket.Server.Infrastructure.Data.PersistenceModels;

internal class Transaction : TimeStampModelBase
{
    public int OrderId { get; set; }
    public long TransactionNumber { get; set; }

    public string ChargeNumber { get; set; }
    public int Status { get; set; }
    public string PaymentMethod { get; set; } = null!;
    public float AmountPaid { get; set; }
    public Order? Order { get; set; }
}