using System.Reflection;

namespace SunRaysMarket.Shared.Core.DomainModels;

public class CustomerPreferences
{
    public int? PreferredStoreId { get; set; }

    public static PropertyInfo GetPropertyInfo(string key)
    {
        return key switch
        {
            "store" => GetPropertyInfo("PreferredStoreId"),
            _ => throw new ArgumentOutOfRangeException(nameof(key), key, null)
        };
    }
}
