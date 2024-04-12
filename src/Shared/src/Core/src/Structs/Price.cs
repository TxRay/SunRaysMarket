using SunRaysMarket.Shared.Core.Utilities;

namespace SunRaysMarket.Shared.Core.Structs;

public struct Price(float value)
{
    public float Raw { get; set; } = value;

    public override string ToString()
    {
        return FormatHelpers.ToCurrencyString(Raw);
    }

    public static Price operator +(Price p1, Price p2)
    {
        return new Price(p1.Raw + p2.Raw);
    }

    public static bool operator >(Price p, double compare)
    {
        return p.Raw > compare;
    }

    public static bool operator <(Price p, double compare)
    {
        return p.Raw < compare;
    }

    public bool Equals(Price other)
    {
        return Math.Abs(Raw - other.Raw) < 0.005;
    }
}