using SunRaysMarket.Server.Application.Preferences;
using SunRaysMarket.Server.Application.Services;
using SunRaysMarket.Server.Application.UnitOfWork;
using SunRaysMarket.Shared.Services.Interfaces;

namespace SunRaysMarket.Server.Application.ServicesImpl.Scoped;

public class StoreLocationsService(
    ICustomerPreferencesService customerPreferencesService,
    ICookieService cookieService,
    IUnitOfWork unitOfWork
) : IStoreLocationService
{
    public Task<IEnumerable<StoreListModel>> GetStoreLocationsAsync()
    {
        return unitOfWork.StoreRepository.GetAllStoresAsync();
    }

    public Task SetPreferredStoreAsync(int storeId)
    {
        cookieService.Preferences!.PreferredStoreId = storeId;

        return Task.CompletedTask;
    }

    public Task<int?> GetPreferredStoreAsync()
    {
        return Task.FromResult(
            cookieService.Preferences?.PreferredStoreId ?? DefaultPreferences.Model.PreferredStoreId
            );
    }
}
