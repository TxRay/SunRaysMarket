using SunRaysMarket.Server.Infrastructure.Data.PersistenceModels.Base;

namespace SunRaysMarket.Server.Infrastructure.Data.PersistenceModels;

internal class UnitOfMeasure : ModelBase
{
    public string Name { get; set; } = default!;
    public string Symbol { get; set; } = default!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
