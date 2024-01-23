using SunRaysMarket.Server.Infrastructure.Data;
using SunRaysMarket.Server.Infrastructure.Data.PersistenceModels;

namespace SunRaysMarket.Server.Infrastructure.Seeding;

internal interface ITimeSlotSeeder : ISeeder { }

internal class TimeSlotSeeder(ApplicationDbContext dbContext) : ITimeSlotSeeder
{
    public async Task SeedAsync()
    {
        if (dbContext.TimeSlots.Any())
            return;

        var rnd = new Random();

        var stores = dbContext.Stores.ToList();
        var timeSlotDefinitions = dbContext.TimeSlotDefinitions.ToList();

        var timeSlots = stores.SelectMany(
            store =>
                timeSlotDefinitions.Select(
                    timeSlotDefinition =>
                        new TimeSlot
                        {
                            StoreId = store.Id,
                            TimeSlotDefinitionId = timeSlotDefinition.Id,
                            Date = DateOnly.FromDateTime(DateTime.Now),
                            Capacity = rnd.Next(5, 10),
                            Filled = 0
                        }
                )
        );

        await dbContext.TimeSlots.AddRangeAsync(timeSlots);
        await dbContext.SaveChangesAsync();
    }
}
