using Application.DomainModels.BaseModels;

namespace Application.DomainModels;

public class UnitOfMeasureListModel : BaseDomainModel
{
    public string Name { get; init; } = default!;
    public string Symbol { get; init; } = default!;

    public override string ToString() => $"{Name} ({Symbol})";
}
