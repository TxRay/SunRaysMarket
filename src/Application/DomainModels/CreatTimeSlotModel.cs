using Application.Structs;

namespace Application.DomainModels;

public class CreatTimeSlotModel
{
    public int StoreId { get; init; }
    public TimeSlotStruct TimeSlotDefinition { get; init; } = default!;
    public int Capacity { get; init; } = default!;
}
