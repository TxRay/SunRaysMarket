namespace Application.DomainModels;

public class CreateTransactionModel
{
    public int Code { get; init; } = default!;
    public int CustomerId { get; init; } = default!;
    public int OrderId { get; init; } = default!;
    public int Status { get; init; } = default!;
    public string? PaymentMethod { get; init; }
    public float AmountPaid { get; init; }
}
