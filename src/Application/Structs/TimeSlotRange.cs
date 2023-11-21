namespace Application.Structs;

public struct TimeSlotRange
{
    public Time Start { get; init; }
    public Time End { get; init; }

    public static TimeSlotRange Create(DateTime startTime, DateTime endTime) =>
        new TimeSlotRange
        {
            Start = Time.FromDateTime(startTime),
            End = Time.FromDateTime(endTime)
        };

    public override string ToString()
    {
        return $"{Start.ToString(false)} - {End.ToString()}";
    }
}
