using Microsoft.Extensions.Logging;
using SunRaysMarket.Server.Application.Repositories;
using SunRaysMarket.Server.Infrastructure.ModelMappings;

namespace SunRaysMarket.Server.Infrastructure.Repositories;

internal class ProductRepository(ApplicationDbContext context, ILogger<ProductRepository> logger)
    : IProductRepository
{
    private readonly ILogger<ProductRepository> _logger = logger;

    public IAsyncEnumerable<ProductListModel> GetAllAsync()
    {
        return context
            .Products
            .Include(p => p.ProductType)
            .ThenInclude(pt => pt!.Department)
            .Include(p => p.InventoryItems)
            .AsProductAsyncEnumerable();
    }

    public IAsyncEnumerable<ProductListModel> GetAllAsync(string listTitle, int? storeId)
    {
        return context
            .Lists
            .Include(l => l.Products)
            .ThenInclude(p => p.ProductType)
            .ThenInclude(pt => pt!.Department)
            .Include(l => l.Products)
            .ThenInclude(p => p.InventoryItems)
            .Where(l => l.Title == listTitle)
            .SelectMany(l => l.Products)
            .AsProductAsyncEnumerable();
    }

    public IAsyncEnumerable<ProductListModel> GetAllAsync(string listType, int departmentId)
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<ProductListModel> GetAllAsync(int departmentId)
    {
        return context
            .Departments
            .Include(d => d.ProductTypes)
            .ThenInclude(pt => pt.Products)
            .ThenInclude(p => p.UnitOfMeasure)
            .Where(d => d.Id == departmentId)
            .SelectMany(d => d.ProductTypes)
            .SelectMany(pt => pt.Products)
            .AsProductAsyncEnumerable();
    }

    public async Task<IEnumerable<ProductListModel>> GetAllSearchAsync(string? queryString)
    {
        if (queryString is null)
            return [];

        var queryStringLowered = queryString.ToLower();

        var query = context
            .Products
            .Include(p => p.ProductType)
            .ThenInclude(pt => pt!.Department)
            .Where(
                p =>
                    p.Description.ToLower().Contains(queryStringLowered)
                    || p.Name.ToLower().Contains(queryStringLowered)
                    || p.ProductType!.Department!.Name.ToLower().Contains(queryStringLowered)
            );

        return await query.ToProductListAsync();
    }

    public async Task<ProductDetailsModel?> GetAsync(int id)
    {
        return await context
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
    }

    public async Task<CreateProductModel?> GetForEditAsync(int id)
    {
        return await context
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
    }

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
            UnitOfMeasureId = model.UnitOfMeasureId
        };

        context.Products.Add(product);

        return Task.CompletedTask;
    }

    public async Task UpdateAsync(UpdateProductModel model)
    {
        var product = await context.Products.FindAsync(model.Id);

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
        var product = await context.Products.FindAsync(id);

        if (product is not null)
            context.Products.Remove(product);
    }
}
