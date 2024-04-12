using SunRaysMarket.Shared.Core.DomainModels;

namespace SunRaysMarket.Shared.Core.Builders;

public interface IAddressBuilder
{
    void WithNewAddress(CreateAddressModel address);
    void WithExistingAddress(int addressId);
}