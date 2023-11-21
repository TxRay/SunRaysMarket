using Infrastructure.Data.PersistenceModels.Base;

namespace Infrastructure.Data.PersistenceModels;

internal class Department : BaseModel
{
    public string Name { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public string Description { get; set; } = default!;

    public virtual ICollection<List> Lists { get; set; } = new List<List>();
    public virtual ICollection<ProductType> ProductTypes { get; set; } = new List<ProductType>();
}
