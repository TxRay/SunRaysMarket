using Application.DomainModels;

namespace Application.Services;

public interface ICustomerAddressService
{
    Task<IEnumerable<AddressModel>> GetAddressesAsync();
    Task<int?> AddAddress(CreateAddressModel model);
    Task RemoveAddress(int addressId);
}