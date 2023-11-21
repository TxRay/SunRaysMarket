using Infrastructure.Data.PersistenceModels.Base;

namespace Infrastructure.Data.PersistenceModels;

internal class List : TimeStampBaseModel
{
    public string Title { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int? DepartmentId { get; set; }

    public Department? Department { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
