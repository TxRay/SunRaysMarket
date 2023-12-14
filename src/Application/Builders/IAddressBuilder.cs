using Application.DomainModels;

namespace Application.Builders;

public interface IAddressBuilder
{
    void WithNewAddress(CreateAddressModel address);
    void WithExistingAddress(int addressId);
}
