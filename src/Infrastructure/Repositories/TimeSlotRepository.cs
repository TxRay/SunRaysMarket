using Application.DomainModels;
using Application.Enums;
using Application.Repositories;

namespace Infrastructure.Repositories;

public class TimeSlotRepository : ITimeSlotRepository
{
    public Task<IEnumerable<TimeSlotDefinitionListModel>> GetAllTimeSlotDefinitionsAsync(
        OrderType orderType
    )
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TimeSlotListModel>> GetAllTimeSlotsAsync(
        int storeId,
        OrderType orderType
    )
    {
        throw new NotImplementedException();
    }

    public Task<TimeSlotModel?> GetTimeSlotAsync(int timeSlotId, OrderType orderType)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CreateTimeSlotAsync(CreatTimeSlotModel model)
    {
        throw new NotImplementedException();
    }
}
