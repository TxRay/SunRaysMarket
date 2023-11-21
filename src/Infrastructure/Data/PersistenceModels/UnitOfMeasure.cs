using Infrastructure.Data.PersistenceModels.Base;

namespace Infrastructure.Data.PersistenceModels;

internal class UnitOfMeasure : BaseModel
{
    public string Name { get; set; } = default!;
    public string Symbol { get; set; } = default!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
