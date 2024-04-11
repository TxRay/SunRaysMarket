using System.Text.Json.Serialization;

namespace SunRaysMarket.Shared.Core.Structs;

[JsonSerializable(typeof(Time))]
public class Time
{
    public int Minutes { get; init; }

    public static Time FromDateTime(DateTime dateTime)
    {
        return new Time { Minutes = dateTime.Hour * 60 + dateTime.Minute };
    }

    public TimeOnly ToTimeOnly()
    {
        return new TimeOnly(Minutes / 60, Minutes % 60, 0);
    }

    public string ToString(bool showMeridian = true)
    {
        return ToTimeOnly().ToString(showMeridian ? "h:mm tt" : "h:mm");
    }
}
