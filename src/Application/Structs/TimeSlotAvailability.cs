namespace Application.Structs;

public struct TimeSlotAvailability
{
    public int Capacity { get; init; }
    public int Filled { get; init; }

    public int Free => Capacity - Filled;

    public bool IsFull => Free == 0;

    public bool IsEmpty => Filled == 0;
}
