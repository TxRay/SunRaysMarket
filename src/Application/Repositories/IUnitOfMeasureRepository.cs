using Application.DomainModels;

namespace Application.Repositories;

public interface IUnitOfMeasureRepository
{
    Task<IEnumerable<UnitOfMeasureListModel>> GetAllAsync();
}
