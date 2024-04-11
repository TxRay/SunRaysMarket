namespace SunRaysMarket.Client.Web.Components.Stores;

public interface IBaseStore
{
    Task<T?> TryGetValueAsync<T>(string key);
    Task SetValueAsync<T>(string key, T value);
    Task RemoveValueAsync(string key);
    Task ClearAllAsync();

    event Action? OnChange;
}

public interface IStore : IBaseStore, IAsyncDisposable
{
    string? KeyPrefix { get; }
}
