using SunRaysMarket.Server.Application.Repositories;

namespace SunRaysMarket.Server.Infrastructure.Repositories;

internal class DepartmentRepository : IDepartmentRepository
{
    private readonly ApplicationDbContext _dbContext;

    public DepartmentRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<DepartmentListModel>> GetAllAsync()
    {
        return await _dbContext
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
    }

    public async Task<UpdateDepartmentModel?> GetForEditAsync(int id)
    {
        return await _dbContext
            .Departments
            .Where(d => d.Id == id)
            .Select(
                d =>
                    new UpdateDepartmentModel
                    {
                        Id = d.Id,
                        Name = d.Name,
                        Description = d.Description
                    }
            )
            .FirstOrDefaultAsync();
    }

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

    public async Task UpdateAsync(UpdateDepartmentModel model)
    {
        var department = await _dbContext.Departments.FindAsync(model.Id);

        if (department is null)
            return;

        department.Name = model.Name;
        department.Slug = Slugs.CreateSlug(model.Name);
        department.Description = model.Description;
    }

    public async Task DeleteAsync(int id)
    {
        var department = await _dbContext.Departments.FindAsync(id);

        if (department is not null)
            _dbContext.Remove(department);
    }
}