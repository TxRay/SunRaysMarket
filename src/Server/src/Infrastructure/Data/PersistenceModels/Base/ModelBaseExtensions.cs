namespace SunRaysMarket.Server.Infrastructure.Data.PersistenceModels.Base;

internal static class ModelBaseExtensions
{
    public static void Update<TEntity>(this TEntity model, Action<TEntity> updater)
        where TEntity : ModelBase, new()
    {
        var tempModel = new TEntity();
        updater(tempModel);

        foreach (var propertyInfo in model.GetType().GetProperties())
        {
            var tempProperty = tempModel.GetType().GetProperty(propertyInfo.Name);
        }
    }
}
