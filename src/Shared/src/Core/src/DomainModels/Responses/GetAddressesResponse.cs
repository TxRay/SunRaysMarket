namespace SunRaysMarket.Shared.Core.DomainModels.Responses;

public record GetAddressesResponse
{
    public IEnumerable<AddressModel> Addresses { get; init; } = [];
}