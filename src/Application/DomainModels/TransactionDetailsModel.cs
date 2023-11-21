namespace Application.DomainModels;

public class TransactionDetailsModel
{
    public int Code { get; init; } = default!;
    public int CustomerId { get; init; } = default!;
    public string CustomerName { get; init; } = default!;
    public int OrderId { get; init; } = default!;
    public int Status { get; init; } = default!;
    public string? PaymentMethod { get; init; }
    public string? AmountPaid { get; init; }
}
