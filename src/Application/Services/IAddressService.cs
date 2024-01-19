using Application.DomainModels;

namespace Application.Services;

/// <summary>
/// Handles
/// </summary>
public interface IAddressService
{
    Task<int?> CreateAddressAsync(CreateAddressModel model);
    Task<AddressModel?> GetAddressAsync(int addressId);
}
