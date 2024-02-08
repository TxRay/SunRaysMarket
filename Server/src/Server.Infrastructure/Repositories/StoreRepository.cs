using SunRaysMarket.Server.Application.Repositories;

namespace SunRaysMarket.Server.Infrastructure.Repositories;

internal class StoreRepository(ApplicationDbContext dbContext) : IStoreRepository
{
    public async Task<IEnumerable<StoreListModel>> GetAllStoresAsync() =>
        await dbContext
            .Stores
            .Select(s => new StoreListModel { Id = s.Id, LocationName = s.LocationName })
            .ToListAsync();
}
