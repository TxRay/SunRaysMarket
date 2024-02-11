namespace SunRaysMarket.Server.Infrastructure.Seeding;

internal interface IStoreSeeder : ISeeder
{
}

internal class StoreSeeder(ApplicationDbContext dbContext) : IStoreSeeder
{
    public async Task SeedAsync()
    {
        if (dbContext.Stores.Any())
            return;

        var stores = new List<Store>
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
        };

        await dbContext.Stores.AddRangeAsync(stores);
        await dbContext.SaveChangesAsync();
    }
}