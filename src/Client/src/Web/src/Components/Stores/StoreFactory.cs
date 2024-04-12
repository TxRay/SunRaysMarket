namespace SunRaysMarket.Client.Web.Components.Stores;

public class StoreFactory(IServiceProvider serviceProvider) : IStoreFactory
{
    public IStore Create<TStore>(string? keyPrefix)
        where TStore : IStore
    {
        var scope = serviceProvider.CreateScope();
        var prefix = keyPrefix ?? typeof(TStore).Name;
        var store = ActivatorUtilities.CreateInstance<TStore>(scope.ServiceProvider, prefix);

        scope.Dispose();

        return store;
    }
}