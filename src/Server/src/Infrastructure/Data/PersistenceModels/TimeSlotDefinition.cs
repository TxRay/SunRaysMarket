using SunRaysMarket.Server.Infrastructure.Data.PersistenceModels.Base;

namespace SunRaysMarket.Server.Infrastructure.Data.PersistenceModels;

internal class TimeSlotDefinition : ModelBase
{
    public int StartTimeMinutes { get; set; }
    public int EndTimeMinutes { get; set; }
    public OrderType OrderType { get; set; }
}
