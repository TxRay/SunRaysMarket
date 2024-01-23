using SunRaysMarket.Shared.Core.DomainModels.BaseModels;

namespace SunRaysMarket.Shared.Core.DomainModels;

public class TransactionDetailsModel : BaseDomainModel
{
    public int CustomerId { get; init; }
    public long TransactionNumber { get; init; }

    public long OrderNumber { get; init; }
    public string CustomerName { get; init; } = default!;
    public int OrderId { get; init; }
    public int Status { get; init; }
    public string? PaymentMethod { get; init; }
    public float? AmountPaid { get; init; }
}
