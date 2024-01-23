namespace SunRaysMarket.Shared.Core.DomainModels.Payment;

public class CreateChargeModel
{
    public string Currency { get; set; } = null!;
    public long Amount { get; set; }
    public string Source { get; set; } = null!;
    public string? CustomerPaymentId { get; set; }

    public static CreateChargeModel CreateCreditCardCharge(
        string currency,
        long amount,
        string cardNumber,
        string customerPaymentId
    )
    {
        return new CreateChargeModel
        {
            Currency = currency,
            Amount = amount,
            Source = cardNumber,
            CustomerPaymentId = customerPaymentId
        };
    }
}
