namespace SunRaysMarket.Shared.Services.Builders;

public interface IAddressBuilder
{
    void WithNewAddress(CreateAddressModel address);
    void WithExistingAddress(int addressId);
}
