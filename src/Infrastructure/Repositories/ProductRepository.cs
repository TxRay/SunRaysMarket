using Application.DomainModels;
using Application.Repositories;
using Application.Utilities;
using Infrastructure.Data;
using Infrastructure.Data.PersistenceModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories;

internal class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ProductRepository> _logger;

    public ProductRepository(ApplicationDbContext context, ILogger<ProductRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<ProductListModel>> GetAllAsync() =>
        await _context
            .Products
            .Include(p => p.ProductType)
            .ThenInclude(pt => pt!.Department)
            .Include(p => p.InventoryItems)
            .Select(
                p =>
                    new ProductListModel
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Slug = p.Slug,
                        PhotoUrl = p.PhotoUrl,
                        Price = p.Price,
                        DiscountPercent = p.DiscountPercent,
                        DepartmentId = p.ProductType!.DepartmentId,
                        DepartmentName = p.ProductType!.Department!.Name,
                        DepartmentSlug = p.ProductType!.Department!.Slug,
                        InStock = p.InventoryItems.Any(i => i.Quantity > 0)
                    }
            )
            .ToListAsync();

    public async Task<IEnumerable<ProductListModel>> GetAllAsync(string listTitle, int? storeId) =>
        await _context
            .Lists
            .Include(l => l.Products)
            .ThenInclude(p => p.ProductType)
            .ThenInclude(pt => pt!.Department)
            .Include(l => l.Products)
            .ThenInclude(p => p.InventoryItems)
            .Where(l => l.Title == listTitle)
            .SelectMany(l => l.Products)
            .Select(
                p =>
                    new ProductListModel
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Slug = p.Slug,
                        PhotoUrl = p.PhotoUrl,
                        Price = p.Price,
                        DiscountPercent = p.DiscountPercent,
                        DepartmentId = p.ProductType!.DepartmentId,
                        DepartmentName = p.ProductType!.Department!.Name,
                        DepartmentSlug = p.ProductType!.Department!.Slug,
                        InStock = p.InventoryItems.Any(i => i.StoreId == storeId && i.Quantity > 0)
                    }
            )
            .ToListAsync();

    public Task<IEnumerable<ProductListModel>> GetAllAsync(string listType, int departmentId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<ProductListModel>> GetAllAsync(int departmentId) =>
        await _context
            .Departments
            .Include(d => d.ProductTypes)
            .ThenInclude(pt => pt.Products)
            .Where(d => d.Id == departmentId)
            .SelectMany(d => d.ProductTypes)
            .SelectMany(pt => pt.Products)
            .Select(
                p =>
                    new ProductListModel
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Slug = p.Slug,
                        PhotoUrl = p.PhotoUrl,
                        Price = p.Price,
                        DiscountPercent = p.DiscountPercent,
                        DepartmentId = p.ProductType!.DepartmentId,
                        DepartmentName = p.ProductType!.Department!.Name,
                        DepartmentSlug = p.ProductType!.Department!.Slug,
                        InStock = p.InventoryItems.Any(i => i.Quantity > 0)
                    }
            )
            .ToListAsync();

    public Task<IEnumerable<ProductListModel>> GetAllSearchAsync(string searchQuery)
    {
        throw new NotImplementedException();
    }

    public async Task<ProductDetailsModel?> GetAsync(int id) =>
        await _context
            .Products
            .Include(p => p.ProductType)
            .ThenInclude(pt => pt!.Department)
            .Include(p => p.InventoryItems)
            .Include(p => p.UnitOfMeasure)
            .Where(p => p.Id == id)
            .Select(
                p =>
                    new ProductDetailsModel
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Description = p.Description,
                        PhotoUrl = p.PhotoUrl,
                        Price = p.Price,
                        DiscountPercent = p.DiscountPercent,
                        DepartmentId = p.ProductType!.DepartmentId,
                        DepartmentName = p.ProductType!.Department!.Name,
                        DepartmentSlug = p.ProductType!.Department!.Slug,
                        ProductTypeId = p.ProductTypeId,
                        ProductTypeName = p.ProductType!.Name,
                        ProductTypeSlug = p.ProductType!.Slug,
                        Measure = p.Measure,
                        UnitOfMeasure = p.UnitOfMeasure!.Symbol,
                        InStock = p.InventoryItems.Any(i => i.Quantity > 0),
                        CreatedAt = p.CreatedAt.GetValueOrDefault(),
                        UpdatedAt = p.UpdatedAt.GetValueOrDefault()
                    }
            )
            .FirstOrDefaultAsync();

    public async Task<CreateProductModel?> GetForEditAsync(int id) =>
        await _context
            .Products
            .Where(p => p.Id == id)
            .Select(
                p =>
                    new CreateProductModel
                    {
                        Name = p.Name,
                        Description = p.Description,
                        PhotoUrl = p.PhotoUrl,
                        DiscountPercent = p.DiscountPercent,
                        Measure = p.Measure,
                        Price = p.Price,
                        ProductTypeId = p.ProductTypeId,
                        UnitOfMeasureId = p.UnitOfMeasureId
                    }
            )
            .FirstOrDefaultAsync();

    public Task CreateAsync(CreateProductModel model)
    {
        var product = new Product
        {
            Name = model.Name,
            Description = model.Description,
            Slug = Slugs.CreateSlug(model.Name),
            PhotoUrl = model.PhotoUrl,
            Price = model.Price,
            DiscountPercent = model.DiscountDecimal,
            ProductTypeId = model.ProductTypeId,
            Measure = model.Measure,
            UnitOfMeasureId = model.UnitOfMeasureId,
        };

        _context.Products.Add(product);

        return Task.CompletedTask;
    }

    public async Task UpdateAsync(UpdateProductModel model)
    {
        var product = await _context.Products.FindAsync(model.Id);

        if (product is null)
            return;

        product.Name = model.Name;
        product.Description = model.Description;
        product.Slug = Slugs.CreateSlug(model.Name);
        product.PhotoUrl = model.PhotoUrl;
        product.Price = model.Price;
        product.DiscountPercent = model.DiscountPercent;
        product.ProductTypeId = model.ProductTypeId;
        product.Measure = model.Measure;
        product.UnitOfMeasureId = model.UnitOfMeasureId;
    }

    public async Task DeleteAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product is not null)
            _context.Products.Remove(product);
    }
}
