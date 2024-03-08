using Microsoft.Extensions.Logging;

namespace SunRaysMarket.Server.Infrastructure.Seeding;

internal sealed class TimeSlotSeeder(ApplicationDbContext dbContext, ILogger<TimeSlotSeeder> logger)
    : SeederBase<TimeSlot>(dbContext, logger)
{
    protected override SeederData RenderSeederData()
    {
        var rnd = new Random();

        var stores = DbContext.Stores.ToList();
        var timeSlotDefinitions = DbContext.TimeSlotDefinitions.ToList();

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

        return new SeederData.EnumerableSeederData(timeSlots);
    }
}
