using SunRaysMarket.Shared.Core.DomainModels.BaseModels;

namespace SunRaysMarket.Shared.Core.DomainModels;

public class DepartmentListModel : BaseDomainModel
{
    public string Name { get; init; } = default!;
    public string Slug { get; init; } = default!;
}