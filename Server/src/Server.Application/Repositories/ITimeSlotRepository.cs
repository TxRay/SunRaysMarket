namespace SunRaysMarket.Server.Application.Repositories;

public interface ITimeSlotRepository
{
    Task<IEnumerable<TimeSlotDefinitionListModel>> GetAllTimeSlotDefinitionsAsync(
        OrderType orderType
    );

    Task<IEnumerable<TimeSlotListModel>> GetAllTimeSlotsAsync(int storeId, OrderType orderType);
    Task<TimeSlotModel?> GetTimeSlotAsync(int timeSlotId);
    Task<bool> CreateTimeSlotAsync(CreatTimeSlotModel model);
}
