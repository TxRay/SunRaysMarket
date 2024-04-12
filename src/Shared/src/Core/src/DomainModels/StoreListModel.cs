using SunRaysMarket.Shared.Core.DomainModels.BaseModels;

namespace SunRaysMarket.Shared.Core.DomainModels;

public class StoreListModel : BaseDomainModel
{
    public string LocationName { get; set; } = default!;
}