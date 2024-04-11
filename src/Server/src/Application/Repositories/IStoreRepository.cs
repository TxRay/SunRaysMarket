namespace SunRaysMarket.Server.Application.Repositories;

public interface IStoreRepository
{
    Task<IEnumerable<StoreListModel>> GetAllStoresAsync();
}
