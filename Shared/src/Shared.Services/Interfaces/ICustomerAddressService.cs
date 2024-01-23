namespace SunRaysMarket.Shared.Services.Interfaces;

public interface ICustomerAddressService
{
    Task<IEnumerable<AddressModel>> GetAddressesAsync();
    Task<int?> AddAddress(CreateAddressModel model);
    Task RemoveAddress(int addressId);
}
