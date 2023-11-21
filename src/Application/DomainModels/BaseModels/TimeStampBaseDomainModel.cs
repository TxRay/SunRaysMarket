using Application.DomainModels.BaseModels;

namespace Application.BaseModels;

public abstract class TimeStampBaseDomainModel : BaseDomainModel
{
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
}
