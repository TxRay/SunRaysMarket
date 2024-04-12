using SunRaysMarket.Server.Infrastructure.Data.PersistenceModels.Base;

namespace SunRaysMarket.Server.Infrastructure.Data.PersistenceModels;

internal class ProductType : TimeStampModelBase
{
    public int DepartmentId { get; set; }
    public string Name { get; set; } = default!;
    public string Slug { get; set; } = default!;

    public string Description { get; set; } = default!;

    public Department? Department { get; set; }
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}