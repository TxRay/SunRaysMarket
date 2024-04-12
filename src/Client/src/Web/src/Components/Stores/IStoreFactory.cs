namespace SunRaysMarket.Client.Web.Components.Stores;

public interface IStoreFactory
{
    IStore Create<TStore>(string? keyPrefix)
        where TStore : IStore;
}