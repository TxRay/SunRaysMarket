namespace SunRaysMarket.Shared.Services.Interfaces;

public interface ICustomerPreferencesService
{
    Task SetPreferredStoreAsync(int storeId);
    Task<int?> GetPreferredStoreAsync();
}