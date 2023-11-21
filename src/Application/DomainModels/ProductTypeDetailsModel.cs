using Application.DomainModels.BaseModels;

namespace Application.DomainModels;

public class ProductTypeDetailsModel : BaseDomainModel
{
    public int DepartmentId { get; init; }
    public string DepartmentName { get; init; } = default!;
    public string DepartmentSlug { get; init; } = default!;
    public string Name { get; init; } = default!;
    public string Slug { get; init; } = default!;
}
