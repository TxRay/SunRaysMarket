using Application.Enums;

namespace WebClient.Models;

public class TimeSlotStepModel
{
    public int SelectedTimeSlotId { get; set; }
    public OrderType SelectedOrderType { get; set; }
}
