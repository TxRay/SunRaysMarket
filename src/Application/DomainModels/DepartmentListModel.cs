using Application.DomainModels.BaseModels;

namespace Application.DomainModels;

public class DepartmentListModel : BaseDomainModel
{
    public string Name { get; init; } = default!;
    public string Slug { get; init; } = default!;
}
