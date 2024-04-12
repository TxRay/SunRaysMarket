namespace SunRaysMarket.Shared.Core.Utilities;

public static class DateTimeExtensions
{
    public static string FormatTimestamp(this DateTime dateTime)
    {
        var currentDateTime = DateTime.UtcNow;
        var timeSpan = currentDateTime - dateTime;

        return timeSpan.TotalDays switch
        {
            0 => $"{timeSpan.TotalHours} hours ago",
            1 => "yesterday",
            < 21 => $"{timeSpan.TotalDays} days ago",
            _ => dateTime.ToString("MMMM dd, yyyy")
        };
    }
}