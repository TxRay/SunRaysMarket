using SunRaysMarket.Shared.Core.DomainModels.BaseModels;

namespace SunRaysMarket.Server.Core.DomainModels;

public class UnitOfMeasureListModel : BaseDomainModel
{
    public string Name { get; init; } = default!;
    public string Symbol { get; init; } = default!;

    public override string ToString()
    {
        return $"{Name} ({Symbol})";
    }
}
