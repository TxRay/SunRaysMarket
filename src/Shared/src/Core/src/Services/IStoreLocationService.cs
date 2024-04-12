using SunRaysMarket.Shared.Core.DomainModels;

namespace SunRaysMarket.Shared.Core.Services;

public interface IStoreLocationService
{
    Task<IEnumerable<StoreListModel>> GetStoreLocationsAsync();
    Task SetPreferredStoreAsync(int storeId);
    Task<int?> GetPreferredStoreAsync();
}