namespace SunRaysMarket.Server.Core.DomainModels.Payment;

public class ChargeResponseModel
{
    public string Id { get; set; } = null!;
    public string CustomerId { get; set; } = null!;
    public long Amount { get; set; }
    public long AmountCaptured { get; set; }
    public string Currency { get; set; } = null!;
    public string Description { get; set; } = null!;
}