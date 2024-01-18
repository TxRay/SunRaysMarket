using System.Text.Json;
using Microsoft.JSInterop;

namespace WebClient.Stores;

public class BrowserSessionStore(IJSRuntime jsRuntime) : IStore
{
    private Lazy<IJSObjectReference> _accessor = new();

    private async Task<bool> InitializeAccessor()
    {
        if (_accessor.IsValueCreated is true)
            return true;

        try
        {
            _accessor = new Lazy<IJSObjectReference>(
                await jsRuntime.InvokeAsync<IJSObjectReference>("import", "/js/session.js")
            );
            return true;
        }
        catch (InvalidOperationException)
        {
            return false;
        }
    }

    private string PrefixKey(string key) => $"{KeyPrefix}:{key}";

    public async Task<T?> TryGetValueAsync<T>(string key)
    {
        if (!await InitializeAccessor())
            return default;

        try
        {
            var valueJson = await _accessor.Value.InvokeAsync<string>("get", PrefixKey(key));
            return JsonSerializer.Deserialize<T>(valueJson);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return default;
        }
    }

    public async Task SetValueAsync<T>(string key, T value)
    {
        if (await InitializeAccessor())
        {
            var serializedValue = JsonSerializer.Serialize(value);
            await _accessor.Value.InvokeVoidAsync("set", PrefixKey(key), serializedValue);
            NotifyStateChanged();
        }
    }

    public async Task RemoveValueAsync(string key)
    {
        if (await InitializeAccessor())
        {
            await _accessor.Value.InvokeVoidAsync("remove", PrefixKey(key));
            NotifyStateChanged();
        }
    }

    public async Task ClearAllAsync()
    {
        if (await InitializeAccessor())
        {
            await _accessor.Value.InvokeVoidAsync("clear");
        }
    }

    public event Action? OnChange;

    private void NotifyStateChanged() => OnChange?.Invoke();

    public async ValueTask DisposeAsync()
    {
        if (_accessor.IsValueCreated)
        {
            await _accessor.Value.DisposeAsync();
        }
    }

    public string? KeyPrefix { get; } = "SunRaysMarket.SessionStore";
}
