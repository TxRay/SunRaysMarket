using Application.Enums;
using Infrastructure.Data.PersistenceModels.Base;

namespace Infrastructure.Data.PersistenceModels;

internal class TimeSlotDefinition : BaseModel
{
    public int StartTimeMinutes { get; set; }
    public int EndTimeMinutes { get; set; }
    public OrderType OrderType { get; set; }
}
