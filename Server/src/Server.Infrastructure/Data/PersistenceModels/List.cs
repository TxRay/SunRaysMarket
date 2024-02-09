using SunRaysMarket.Server.Infrastructure.Data.PersistenceModels.Base;

namespace SunRaysMarket.Server.Infrastructure.Data.PersistenceModels;

internal class List : TimeStampModelBase
{
    public string Title { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int? DepartmentId { get; set; }

    public Department? Department { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
