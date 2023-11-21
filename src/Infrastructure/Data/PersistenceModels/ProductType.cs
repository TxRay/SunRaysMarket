using Infrastructure.Data.PersistenceModels.Base;

namespace Infrastructure.Data.PersistenceModels;

internal class ProductType : TimeStampBaseModel
{
    public int DepartmentId { get; set; }
    public string Name { get; set; } = default!;
    public string Slug { get; set; } = default!;

    public string Description { get; set; } = default!;

    public Department? Department { get; set; }
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
