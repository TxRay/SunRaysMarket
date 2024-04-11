using SunRaysMarket.Shared.Core.DomainModels.BaseModels;

namespace SunRaysMarket.Shared.Core.DomainModels;

public class ProductTypeDetailsModel : BaseDomainModel
{
    public int DepartmentId { get; init; }
    public string DepartmentName { get; init; } = default!;
    public string DepartmentSlug { get; init; } = default!;
    public string Name { get; init; } = default!;
    public string Slug { get; init; } = default!;
}
