using SunRaysMarket.Server.Core.DomainModels;

namespace SunRaysMarket.Server.Application.Repositories;

public interface IAddressRepository
{
    Task<bool> CreateAddressAsync(CreateAddressModel model);
    Task<AddressModel?> GetAsync(int id);
    Task<bool> UpdateAsync(UpdateAddressModel model);
    Task<bool> DeleteAsync(int id);

    AddressModel? GetPersisted();

    int? GetPersistedId();
}