using Application.DomainModels;
using Application.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

internal class DepartmentRepository : IDepartmentRepository
{
    private readonly ApplicationDbContext _dbContext;

    public DepartmentRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<DepartmentListModel>> GetAllAsync() =>
        await _dbContext.Departments
            .Select(
                d =>
                    new DepartmentListModel
                    {
                        Id = d.Id,
                        Name = d.Name,
                        Slug = d.Slug
                    }
            )
            .ToListAsync();
}
