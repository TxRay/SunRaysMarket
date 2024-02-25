using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Caching.Distributed;

namespace SunRaysMarket.Server.Infrastructure.Cache;

internal static class DistributedCacheExtensions
{
    private static JsonSerializerOptions SerializerOptions => new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        AllowTrailingCommas = true,
        WriteIndented = false,
        DictionaryKeyPolicy = null
    };

    public static async Task SetValueAsync<T>(this IDistributedCache cache, string key, T value,
        CancellationToken token = default)
        => await cache.SetValueAsync(key, value, new DistributedCacheEntryOptions(), token);

    public static async Task SetValueAsync<T>(this IDistributedCache cache, string key, T value,
        DistributedCacheEntryOptions options, CancellationToken token = default)
    {
        var bytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(value, SerializerOptions));
        await cache.SetAsync(key, bytes, options, token);
    }

    public static bool TryGetValue<T>(this IDistributedCache cache, string key, out T? value)
    {
        var bytes = cache.Get(key);
        value = bytes is not null ? JsonSerializer.Deserialize<T>(bytes, SerializerOptions) : default;

        return bytes is not null;
    }

    public static async Task<T> SetOrFetchAsync<T>(this IDistributedCache cache, string key, Func<Task<T>> set,
        CancellationToken token = default)
        => await cache.SetOrFetchAsync(key, set, new DistributedCacheEntryOptions(), token);


    public static async Task<T> SetOrFetchAsync<T>(this IDistributedCache cache, string key, Func<Task<T>> set,
        DistributedCacheEntryOptions options, CancellationToken token = default)
    {
        if (cache.TryGetValue<T>(key, out var value))
        {
            return value!;
        }

        var fetchedData = await set.Invoke();
        await cache.SetValueAsync(key, fetchedData, options, token);

        return fetchedData;
    }
}