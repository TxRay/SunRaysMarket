using Microsoft.Extensions.Logging;

namespace SunRaysMarket.Server.Infrastructure.Seeding;

internal sealed class StoreSeeder(ApplicationDbContext dbContext, ILogger<StoreSeeder> logger)
    : SeederBase<Store>(dbContext, logger)
{
    protected override SeederData RenderSeederData()
    {
        return new SeederData.EnumerableSeederData(
            new Store[]
            {
                new()
                {
                    LocationName = "North McAllen",
                    PhoneNumber = "123-456-7890",
                    EmailAddress = "mcallen.north@sunraysmarket.com",
                    ManagerName = "Jake Slake"
                },
                new()
                {
                    LocationName = "South McAllen",
                    PhoneNumber = "123-456-7890",
                    EmailAddress = "mcallen.south@sunraysmarket.com",
                    ManagerName = "Jane Dane"
                },
                new()
                {
                    LocationName = "Edinburg",
                    PhoneNumber = "123-456-7890",
                    EmailAddress = "edinburg@sunraysmarket.com",
                    ManagerName = "Dan McMann"
                },
                new()
                {
                    LocationName = "Mission",
                    PhoneNumber = "123-456-7890",
                    EmailAddress = "mission@sunraysmarke.com",
                    ManagerName = "Bill Shill"
                }
            }
        );
    }
}
