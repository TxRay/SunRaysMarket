using Microsoft.Extensions.Caching.Distributed;
using SunRaysMarket.Server.Application.Repositories;
using SunRaysMarket.Server.Infrastructure.Cache;

namespace SunRaysMarket.Server.Infrastructure.Repositories;

internal class StoreRepository(ApplicationDbContext dbContext, IDistributedCache cache) : IStoreRepository
{
    public async Task<IEnumerable<StoreListModel>> GetAllStoresAsync() =>
        await cache.SetOrFetchAsync(
            "GetAllStoreLocations",
            async () => await dbContext
                .Stores
                .Select(s => new StoreListModel { Id = s.Id, LocationName = s.LocationName })
                .ToArrayAsync()
        );
}