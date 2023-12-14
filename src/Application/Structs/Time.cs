using System.Text.Json.Serialization;

namespace Application.Structs;

[JsonSerializable(typeof(Time))]
public class Time
{
    public int Minutes { get; init; }

    public static Time FromDateTime(DateTime dateTime)
    {
        return new Time { Minutes = dateTime.Hour * 60 + dateTime.Minute };
    }

    public TimeOnly ToTimeOnly() => new(Minutes / 60, Minutes % 60, 0);

    public string ToString(bool showMeridian = true) =>
        ToTimeOnly().ToString(showMeridian ? "h:mm tt" : "h:mm");
}
