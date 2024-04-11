using SunRaysMarket.Shared.Core.Enums;

namespace SunRaysMarket.Shared.Core.DomainModels.Checkout;

public class TimeSlotStepModel
{
    public int SelectedTimeSlotId { get; set; }
    public OrderType SelectedOrderType { get; set; }
}
