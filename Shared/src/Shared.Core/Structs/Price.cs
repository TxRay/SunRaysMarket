using System.Text.Json;
using System.Text.Json.Serialization;
using SunRaysMarket.Shared.Core.Utilities;

namespace SunRaysMarket.Shared.Core.Structs;

public struct Price(float value)
{
    public float Raw { get; set; } = value;

    public override string ToString() => FormatHelpers.ToCurrencyString(Raw);

    public static Price operator +(Price p1, Price p2) => new(p1.Raw + p2.Raw);

    public static bool operator >(Price p, double compare) => p.Raw > compare;
    public static bool operator <(Price p, double compare) => p.Raw < compare;

    public bool Equals(Price other) => Math.Abs(Raw - other.Raw) < 0.005;
}
