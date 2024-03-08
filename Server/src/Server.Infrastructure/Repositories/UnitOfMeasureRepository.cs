using SunRaysMarket.Server.Application.Repositories;

namespace SunRaysMarket.Server.Infrastructure.Repositories;

internal class UnitOfMeasureRepository : IUnitOfMeasureRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UnitOfMeasureRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<UnitOfMeasureListModel>> GetAllAsync()
    {
        return await _dbContext
            .UnitsOfMeasure
            .OrderBy(uom => uom.Name)
            .Select(
                uom =>
                    new UnitOfMeasureListModel
                    {
                        Id = uom.Id,
                        Name = uom.Name,
                        Symbol = uom.Symbol
                    }
            )
            .ToListAsync();
    }
}
