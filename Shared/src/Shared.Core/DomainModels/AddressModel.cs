using SunRaysMarket.Shared.Core.DomainModels.BaseModels;

namespace SunRaysMarket.Shared.Core.DomainModels;

public class AddressModel : BaseDomainModel
{
    public string Street { get; init; } = null!;
    public string City { get; init; } = null!;
    public string State { get; init; } = null!;
    public string PostalCode { get; init; } = null!;
    public string Country { get; init; } = null!;
}