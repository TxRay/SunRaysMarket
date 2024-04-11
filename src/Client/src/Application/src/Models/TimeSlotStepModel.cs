using SunRaysMarket.Shared.Core.Enums;

namespace SunRaysMarket.Client.Application.Models;

public class TimeSlotStepModel
{
    public int SelectedTimeSlotId { get; set; }
    public OrderType SelectedOrderType { get; set; }
}
