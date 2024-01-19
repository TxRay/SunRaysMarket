using Application.DomainModels;

namespace Application.EndpointViewModels;

public record GetAddressesResponse
{
    public IEnumerable<AddressModel> Addresses { get; init; } = [];
}
