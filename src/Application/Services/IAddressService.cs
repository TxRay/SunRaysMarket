using Application.DomainModels;

namespace Application.Services;

public interface IAddressService
{
    Task<int?> CreateAddressAsync(CreateAddressModel model);
    Task<AddressModel?> GetAddressAsync(int addressId);
}
