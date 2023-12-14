namespace Application.DomainModels.Payment;

public class CreateCardModel
{
    public string Name { get; set; } = null!;
    public string Number { get; set; } = null!;
    public int ExpiryMonth { get; set; }
    public int ExpiryYear { get; set; }
    public string Cvv { get; set; } = null!;

    public static CreateCardModel Create(string name, string number, string expiry, string cvv)
    {
        var expirySplit = expiry.Split("/");
        var expiryMonth = int.Parse(expirySplit[0]);
        var expiryYear = int.Parse(expirySplit[1]);

        return new CreateCardModel
        {
            Name = name,
            Number = number,
            ExpiryMonth = expiryMonth,
            ExpiryYear = expiryYear,
            Cvv = cvv
        };
    }
}
