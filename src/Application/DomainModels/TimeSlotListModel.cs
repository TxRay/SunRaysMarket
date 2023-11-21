using Application.Structs;

namespace Application.DomainModels;

public class TimeSlotListModel
{
    public int Id { get; init; }
    public TimeSlotStruct TimeSlotDefinition { get; init; } = default!;
    public TimeSlotAvailability Availability { get; init; } = default!;
}
