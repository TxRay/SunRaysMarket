using SunRaysMarket.Shared.Core.Structs;

namespace SunRaysMarket.Shared.Core.DomainModels;

public class TimeSlotDefinitionListModel
{
    public int Id { get; init; }
    public TimeSlotRange TimeSlotRange { get; init; } = default!;
}
