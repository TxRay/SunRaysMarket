using System.Text.Json.Serialization;

namespace SunRaysMarket.Shared.Core.Structs;

[JsonSerializable(typeof(TimeSlotRange))]
public class TimeSlotRange
{
    public Time Start { get; init; }
    public Time End { get; init; }

    public override string ToString()
    {
        return $"{Start.ToString(false)} - {End.ToString()}";
    }
}