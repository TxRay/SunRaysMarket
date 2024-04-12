using SunRaysMarket.Shared.Core.DomainModels;

namespace SunRaysMarket.Shared.Core.Services;

/// <summary>
///     Handles
/// </summary>
public interface IAddressService
{
    Task<int?> CreateAddressAsync(CreateAddressModel model);
    Task<AddressModel?> GetAddressAsync(int addressId);
}