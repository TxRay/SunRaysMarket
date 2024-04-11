using SunRaysMarket.Shared.Core.Structs;

namespace SunRaysMarket.Shared.Core.DomainModels;

public class TimeSlotModel
{
    public int Id { get; init; }
    public int StoreId { get; init; }
    public string StoreName { get; init; } = default!;
    public TimeSlotStruct TimeSlotDefinition { get; init; } = default!;
    public TimeSlotAvailability Availability { get; init; } = default!;
}
