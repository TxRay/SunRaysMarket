using Microsoft.Extensions.Logging;

namespace SunRaysMarket.Server.Infrastructure.Seeding;

internal sealed class TimeSlotDefinitionsSeeder(
    ApplicationDbContext dbContext,
    ILogger<SeederBase<TimeSlotDefinition>> logger)
    : SeederBase<TimeSlotDefinition>(dbContext, logger)
{
    private const int StartHour = 8;
    private const int EndHour = 20;

    protected override SeederData RenderSeederData()
    {
        var timeSlotDefinitions = new List<TimeSlotDefinition>();
        timeSlotDefinitions.AddRange(GenerateTimeSlots(OrderType.Delivery, 60));
        timeSlotDefinitions.AddRange(GenerateTimeSlots(OrderType.Pickup, 30));

        return new SeederData.EnumerableSeederData(timeSlotDefinitions);
    }

    private static IEnumerable<TimeSlotDefinition> GenerateTimeSlots(OrderType orderType, int duration)
    {
        var timeSlots = new List<TimeSlotDefinition>();
        var startMinutes = StartHour * 60;
        var endMinutes = EndHour * 60;

        for (var start = startMinutes; start < endMinutes; start += duration)
        {
            var timeSlot = new TimeSlotDefinition
            {
                StartTimeMinutes = start,
                EndTimeMinutes = start + duration,
                OrderType = orderType
            };
            timeSlots.Add(timeSlot);
        }

        return timeSlots;
    }
}