namespace SunRaysMarket.Shared.Extensions.Streaming;

public class StreamedDataReceiver<TData>(IAsyncEnumerable<TData> stream, ICollection<TData> accumulator)
{
    public async Task ReceiveAsync()
    {
        await foreach (var item in stream)
        {
            accumulator.Add(item);
            OnItemReceived?.Invoke();
            await (OnItemReceivedAsync?.Invoke(item) ?? Task.CompletedTask);
        }
    }
    
    public event Action? OnItemReceived;
    public event Func<TData, Task>? OnItemReceivedAsync;
}