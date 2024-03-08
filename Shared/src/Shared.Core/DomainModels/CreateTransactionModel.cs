namespace SunRaysMarket.Shared.Core.DomainModels;

public class CreateTransactionModel
{
    public int OrderId { get; init; } = default!;
    public string ChargeNumber { get; init; } = default!;
    public int Status { get; init; } = default!;
    public string? PaymentMethod { get; init; }
    public float AmountPaid { get; init; }
}
