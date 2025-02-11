using SunRaysMarket.Server.Application.Repositories;
using SunRaysMarket.Server.Core.DomainModels;
using SunRaysMarket.Shared.Core.Structs;

namespace SunRaysMarket.Server.Infrastructure.Repositories;

internal class TimeSlotRepository(ApplicationDbContext dbContext) : ITimeSlotRepository
{
    public async Task<IEnumerable<TimeSlotDefinitionListModel>> GetAllTimeSlotDefinitionsAsync(
        OrderType orderType
    )
    {
        return await dbContext
            .TimeSlotDefinitions
            .Where(tsd => tsd.OrderType == orderType)
            .Select(
                tsd =>
                    new TimeSlotDefinitionListModel
                    {
                        Id = tsd.Id,
                        TimeSlotRange = new TimeSlotRange
                        {
                            Start = new Time { Minutes = tsd.StartTimeMinutes },
                            End = new Time { Minutes = tsd.EndTimeMinutes }
                        }
                    }
            )
            .ToListAsync();
    }

    public async Task<IEnumerable<TimeSlotListModel>> GetAllTimeSlotsAsync(
        int storeId,
        OrderType orderType
    )
    {
        return await dbContext
            .TimeSlots
            .Include(ts => ts.TimeSlotDefinition)
            .Where(ts => ts.StoreId == storeId && ts.TimeSlotDefinition.OrderType == orderType)
            .Select(
                ts =>
                    new TimeSlotListModel
                    {
                        Id = ts.Id,
                        TimeSlotDefinition = TimeSlotStruct.Create(
                            ts.Date,
                            new TimeSlotRange
                            {
                                Start = new Time
                                {
                                    Minutes = ts.TimeSlotDefinition.StartTimeMinutes
                                },
                                End = new Time { Minutes = ts.TimeSlotDefinition.EndTimeMinutes }
                            }
                        ),
                        Availability = new TimeSlotAvailability
                        {
                            Capacity = ts.Capacity,
                            Filled = ts.Filled
                        }
                    }
            )
            .ToListAsync();
    }

    public Task<TimeSlotModel?> GetTimeSlotAsync(int timeSlotId)
    {
        return dbContext
            .TimeSlots
            .Include(ts => ts.TimeSlotDefinition)
            .Include(ts => ts.Store)
            .Where(ts => ts.Id == timeSlotId)
            .Select(
                ts =>
                    new TimeSlotModel
                    {
                        Id = ts.Id,
                        StoreId = ts.StoreId,
                        StoreName = ts.Store!.LocationName,
                        Availability = new TimeSlotAvailability
                        {
                            Capacity = ts.Capacity,
                            Filled = ts.Filled
                        },
                        TimeSlotDefinition = TimeSlotStruct.Create(
                            ts.Date,
                            new TimeSlotRange
                            {
                                Start = new Time
                                {
                                    Minutes = ts.TimeSlotDefinition!.StartTimeMinutes
                                },
                                End = new Time { Minutes = ts.TimeSlotDefinition.EndTimeMinutes }
                            }
                        )
                    }
            )
            .FirstOrDefaultAsync();
    }

    public Task<bool> CreateTimeSlotAsync(CreatTimeSlotModel model)
    {
        throw new NotImplementedException();
    }
}