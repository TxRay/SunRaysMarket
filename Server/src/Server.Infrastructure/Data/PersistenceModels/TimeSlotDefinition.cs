using SunRaysMarket.Server.Infrastructure.Data.PersistenceModels.Base;
using SunRaysMarket.Shared.Core.Enums;

namespace SunRaysMarket.Server.Infrastructure.Data.PersistenceModels;

internal class TimeSlotDefinition : BaseModel
{
    public int StartTimeMinutes { get; set; }
    public int EndTimeMinutes { get; set; }
    public OrderType OrderType { get; set; }
}
