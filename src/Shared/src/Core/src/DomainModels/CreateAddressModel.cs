namespace SunRaysMarket.Shared.Core.DomainModels;

public class CreateAddressModel
{
    public string Street { get; set; } = null!;
    public string City { get; set; } = null!;
    public string State { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public string Country { get; set; } = "USA";
}
