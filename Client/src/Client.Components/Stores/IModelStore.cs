namespace SunRaysMarket.Client.Components.Stores;

public interface IModelStore
{
}

public interface IModelStore<TKey> : IModelStore
{
    void Add<TModel>(TKey key, TModel model)
        where TModel : class;

    void Remove(TKey key);

    bool TryGet<TModel>(TKey key, out TModel? model)
        where TModel : class;

    TModel? GetOrDefault<TModel>(TKey key)
        where TModel : class;

    void Update<TModel>(TKey key, TModel model)
        where TModel : class;

    IEnumerable<TKey> GetKeys();

    void Clear();
}