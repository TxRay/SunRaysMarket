namespace Application.Structs;

public struct Time
{
    public int Minutes { get; init; }

    public static Time FromDateTime(DateTime dateTime)
    {
        return new Time { Minutes = dateTime.Hour * 60 + dateTime.Minute };
    }

    public DateTime ToDateTime() => new DateTime(0, 0, 0, Minutes / 60, Minutes % 60, 0);

    public string ToString(bool showMeridian = true) =>
        ToDateTime().ToString(showMeridian ? "h:mm tt" : "h:mm");
}
