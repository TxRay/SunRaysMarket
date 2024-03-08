namespace SunRaysMarket.Client.Components.Stores;

public interface IWizardModelStore : IModelStore<string> { }

public class WizardModelStore : IWizardModelStore
{
    private readonly Dictionary<string, object> _models = new();

    public void Add<TModel>(string key, TModel model)
        where TModel : class
    {
        _models.TryAdd(key, model);
    }

    public void Remove(string key)
    {
        _models.Remove(key);
    }

    public bool TryGet<TModel>(string key, out TModel? model)
        where TModel : class
    {
        model = null;
        return _models.TryGetValue(key, out var obj) && (model = obj as TModel) != null;
    }

    public TModel? GetOrDefault<TModel>(string key)
        where TModel : class
    {
        return _models.TryGetValue(key, out var obj) ? obj as TModel : null;
    }

    public void Update<TModel>(string key, TModel model)
        where TModel : class
    {
        _models[key] = model;
    }

    public IEnumerable<string> GetKeys()
    {
        return _models.Keys;
    }

    public void Clear()
    {
        _models.Clear();
    }
}
