using Microsoft.Extensions.Logging;
using SunRaysMarket.Server.Application.Repositories;
using SunRaysMarket.Server.Infrastructure.Data;
using SunRaysMarket.Server.Infrastructure.Data.PersistenceModels;
using SunRaysMarket.Shared.Core.DomainModels;
using SunRaysMarket.Shared.Core.Utilities;

namespace SunRaysMarket.Server.Infrastructure.Repositories;

internal class ProductTypeRepository : IProductTypeRepository
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ProductTypeRepository> _logger;

    public ProductTypeRepository(
        ApplicationDbContext context,
        ILogger<ProductTypeRepository> logger
    )
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<ProductTypeDetailsModel>> GetAllAsync() =>
        await _context
            .ProductTypes
            .Include(x => x.Department)
            .Select(
                x =>
                    new ProductTypeDetailsModel
                    {
                        Id = x.Id,
                        DepartmentId = x.DepartmentId,
                        DepartmentName = x.Department!.Name,
                        DepartmentSlug = x.Department.Slug,
                        Name = x.Name,
                        Slug = x.Slug
                    }
            )
            .ToListAsync();

    public async Task<ProductTypeDetailsModel?> GetAsync(int id) =>
        await _context
            .ProductTypes
            .Where(p => p.Id == id)
            .Select(
                p =>
                    new ProductTypeDetailsModel
                    {
                        DepartmentId = p.DepartmentId,
                        DepartmentName = p.Department!.Name,
                        DepartmentSlug = p.Department.Slug,
                        Name = p.Name,
                        Slug = p.Slug
                    }
            )
            .FirstOrDefaultAsync();

    public async Task<CreateProductTypeModel?> GetForEditAsync(int id) =>
        await _context
            .ProductTypes
            .Where(pt => pt.Id == id)
            .Select(
                pt =>
                    new CreateProductTypeModel
                    {
                        DepartmentId = pt.DepartmentId,
                        Description = pt.Description,
                        Name = pt.Name
                    }
            )
            .FirstOrDefaultAsync();

    public Task CreateAsync(CreateProductTypeModel model)
    {
        var productType = new ProductType
        {
            DepartmentId = model.DepartmentId,
            Name = model.Name,
            Slug = Slugs.CreateSlug(model.Name),
            Description = model.Description
        };

        _context.ProductTypes.Add(productType);

        return Task.CompletedTask;
    }

    public async Task UpdateAsync(UpdateProductTypeModel model)
    {
        var productType = await _context.ProductTypes.FindAsync(model.Id);

        if (productType is null)
            return;

        productType.DepartmentId = model.DepartmentId;
        productType.Name = model.Name;
        productType.Slug = Slugs.CreateSlug(model.Name);
    }

    public async Task DeleteAsync(int id)
    {
        var productType = await _context.ProductTypes.FindAsync(id);

        if (productType is not null)
            _context.ProductTypes.Remove(productType);
        else
            _logger.LogWarning("Product type with id {id} was not found", id);
    }
}
