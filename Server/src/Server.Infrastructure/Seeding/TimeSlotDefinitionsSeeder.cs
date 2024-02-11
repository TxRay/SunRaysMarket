namespace SunRaysMarket.Server.Infrastructure.Seeding;

internal interface ITimeSlotDefinitionsSeeder : ISeeder
{
}

internal class TimeSlotDefinitionsSeeder : ITimeSlotDefinitionsSeeder
{
    private const int StartHour = 8;
    private const int EndHour = 20;
    private readonly ApplicationDbContext _dbContext;

    public TimeSlotDefinitionsSeeder(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SeedAsync()
    {
        if (_dbContext.TimeSlotDefinitions.Any())
            return;

        var timeSlotDefinitions = new List<TimeSlotDefinition>();
        timeSlotDefinitions.AddRange(GenerateTimeSlots(OrderType.Delivery, 60));
        timeSlotDefinitions.AddRange(GenerateTimeSlots(OrderType.Pickup, 30));

        await _dbContext.TimeSlotDefinitions.AddRangeAsync(timeSlotDefinitions);
        await _dbContext.SaveChangesAsync();
    }

    private List<TimeSlotDefinition> GenerateTimeSlots(OrderType orderType, int duration)
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