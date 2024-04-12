namespace SunRaysMarket.Shared.Core.Structs;

public class TimeSlotAvailability
{
    public int Capacity { get; init; }
    public int Filled { get; init; }

    public int Free => Capacity - Filled;

    public bool IsFull => Free == 0;

    public bool IsEmpty => Filled == 0;
}