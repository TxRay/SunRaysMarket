namespace SunRaysMarket.Shared.Services.Interfaces;

public interface IStoreLocationService
{ 
    Task<IEnumerable<StoreListModel>> GetStoreLocationsAsync();
    Task SetPreferredStoreAsync(int storeId);
    Task<int?> GetPreferredStoreAsync();
}