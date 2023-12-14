namespace Application.DomainModels.Checkout;

public class PaymentInfoModel
{
    public string CardNumber { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Expiry { get; set; } = null!;
    public string Cvv { get; set; } = null!;
}
