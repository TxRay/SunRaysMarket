using SunRaysMarket.Server.Core.DomainModels;

namespace SunRaysMarket.Server.Application.Repositories;

public interface IUnitOfMeasureRepository
{
    Task<IEnumerable<UnitOfMeasureListModel>> GetAllAsync();
}
