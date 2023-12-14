using System.Dynamic;
using System.Text.Json.Serialization;

namespace Application.Structs;

public class TimeSlotStruct
{
    public int DayOfWeek { get; init; }
    public int DayOfMonth { get; init; }
    public int Month { get; init; }
    public int Year { get; init; }
    public TimeSlotRange TimeSlotRange { get; init; }

    public static TimeSlotStruct Create(DateOnly date, TimeSlotRange timeSlotRange) =>
        new()
        {
            DayOfWeek = (int)date.DayOfWeek,
            DayOfMonth = date.Day,
            Month = date.Month,
            Year = date.Year,
            TimeSlotRange = timeSlotRange
        };

    public DateTime ToDateTime() => new(Year, Month, DayOfMonth);

    private string DayOfWeekName =>
        DayOfWeek switch
        {
            0 => "Sunday",
            1 => "Monday",
            2 => "Tuesday",
            3 => "Wednesday",
            4 => "Thursday",
            5 => "Friday",
            6 => "Saturday",
            _
                => throw new ArgumentOutOfRangeException(
                    nameof(DayOfWeek),
                    DayOfWeek,
                    "Day of week must be between 0 and 6"
                )
        };

    private string MonthName =>
        Month switch
        {
            1 => "January",
            2 => "February",
            3 => "March",
            4 => "April",
            5 => "May",
            6 => "June",
            7 => "July",
            8 => "August",
            9 => "September",
            10 => "October",
            11 => "November",
            12 => "December",
            _
                => throw new ArgumentOutOfRangeException(
                    nameof(Month),
                    Month,
                    "Month must be between 1 and 12"
                )
        };

    private string DayOfMonthOrdinal =>
        DayOfMonth switch
        {
            1 => "1st",
            2 => "2nd",
            3 => "3rd",
            21 => "21st",
            22 => "22nd",
            23 => "23rd",
            31 => "31st",
            _ => $"{DayOfMonth}th"
        };

    public override string ToString() =>
        $"{DayOfWeekName}, {MonthName} {DayOfMonthOrdinal}, {Year} {TimeSlotRange}";
}
