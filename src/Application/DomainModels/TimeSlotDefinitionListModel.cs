using Application.Structs;

namespace Application.DomainModels;

public class TimeSlotDefinitionListModel
{
    public int Id { get; init; }
    public TimeSlotRange TimeSlotRange { get; init; } = default!;
}
