using SunRaysMarket.Shared.Core.Enums;

namespace SunRaysMarket.Server.Application.Repositories;

public interface ITimeSlotRepository
{
    Task<IEnumerable<TimeSlotDefinitionListModel>> GetAllTimeSlotDefinitionsAsync(
        OrderType orderType
    );
    Task<IEnumerable<TimeSlotListModel>> GetAllTimeSlotsAsync(int storeId, OrderType orderType);
    Task<TimeSlotModel?> GetTimeSlotAsync(int timeSlotId, OrderType orderType);
    Task<bool> CreateTimeSlotAsync(CreatTimeSlotModel model);
}
