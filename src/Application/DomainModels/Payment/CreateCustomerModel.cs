namespace Application.DomainModels.Payment;

public class CreatePaymentCustomerModel
{
    public string Email { get; set; } = null!;
    public string Name { get; set; } = null!;
}
