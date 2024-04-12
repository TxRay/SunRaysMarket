using SunRaysMarket.Shared.Core.DomainModels;

namespace SunRaysMarket.Shared.Core.Services;

public interface ICustomerAddressService
{
    Task<IEnumerable<AddressModel>> GetAddressesAsync();
    Task<int?> AddAddress(CreateAddressModel model);
    Task RemoveAddress(int addressId);
}