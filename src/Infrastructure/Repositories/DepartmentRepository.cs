using Application.DomainModels;
using Application.Repositories;
using Application.Utilities;
using Infrastructure.Data;
using Infrastructure.Data.PersistenceModels;
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
        await _dbContext
            .Departments
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

    public async Task CreateAsync(CreateDepartmentModel model)
    {
        var department = new Department
        {
            Name = model.Name,
            Slug = Slugs.CreateSlug(model.Name),
            Description = model.Description
        };

        await _dbContext.Departments.AddAsync(department);
    }
}
