using Microsoft.Extensions.Caching.Distributed;
using SunRaysMarket.Server.Application.Repositories;
using SunRaysMarket.Server.Infrastructure.Cache;

namespace SunRaysMarket.Server.Infrastructure.Repositories;

internal class DepartmentRepository(ApplicationDbContext dbContext, IDistributedCache cache) : IDepartmentRepository
{
    private const string GetAllCacheKey = "GetAllDepartments";
    
    public async Task<IEnumerable<DepartmentListModel>> GetAllAsync()
        => await cache.SetOrFetchAsync(
            GetAllCacheKey,
            async () => await dbContext
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
                .ToArrayAsync()
        );

    public async Task<UpdateDepartmentModel?> GetForEditAsync(int id)
    {
        return await dbContext
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
        await cache.RemoveAsync(GetAllCacheKey);
        
        var department = new Department
        {
            Name = model.Name,
            Slug = Slugs.CreateSlug(model.Name),
            Description = model.Description
        };

        await dbContext.Departments.AddAsync(department);
    }

    public async Task UpdateAsync(UpdateDepartmentModel model)
    {
        await cache.RemoveAsync(GetAllCacheKey);
        
        var department = await dbContext.Departments.FindAsync(model.Id);

        if (department is null)
            return;

        department.Name = model.Name;
        department.Slug = Slugs.CreateSlug(model.Name);
        department.Description = model.Description;
    }

    public async Task DeleteAsync(int id)
    {
        var department = await dbContext.Departments.FindAsync(id);

        if (department is not null)
            dbContext.Remove(department);
    }
}