namespace SunRaysMarket.Client.Components.Stores;

public interface IStoreFactory
{
    IStore Create<TStore>(string? keyPrefix)
        where TStore : IStore;
}