namespace SunRaysMarket.Client.Application.Models;

public class PaymentInfoModel
{
    public string CardNumber { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Expiry { get; set; } = null!;
    public string Cvv { get; set; } = null!;

    public (int, int) ParseExpiry()
    {
        var expiry = Expiry.Split('/');
        return (int.Parse(expiry[0]), int.Parse(expiry[1]));
    }
}
