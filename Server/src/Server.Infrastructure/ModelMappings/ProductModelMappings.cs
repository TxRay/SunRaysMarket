namespace SunRaysMarket.Server.Infrastructure.ModelMappings;

internal static class ProductModelMappings
{
    public static async Task<ProductDetailsModel?> ToProductDetailsAsync(
        this IQueryable<Product> set
    )
    {
        return await set.Select(
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

    public static async Task<IEnumerable<ProductListModel>> ToProductListAsync(
        this IQueryable<Product> set
    )
    {
        return await set.Select(
                p =>
                    new ProductListModel
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Slug = p.Slug,
                        PhotoUrl = p.PhotoUrl,
                        Measure = p.Measure,
                        UnitOfMeasure = p.UnitOfMeasure!.Symbol,
                        Price = p.Price,
                        DiscountPercent = p.DiscountPercent,
                        DepartmentId = p.ProductType!.DepartmentId,
                        DepartmentName = p.ProductType!.Department!.Name,
                        DepartmentSlug = p.ProductType!.Department!.Slug,
                        InStock = p.InventoryItems.Any(i => i.Quantity > 0)
                    }
            )
            .ToListAsync();
    }

    public static IAsyncEnumerable<ProductListModel> AsProductAsyncEnumerable(
        this IQueryable<Product> set
    )
    {
        return set.Select(
                p =>
                    new ProductListModel
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Slug = p.Slug,
                        PhotoUrl = p.PhotoUrl,
                        Measure = p.Measure,
                        UnitOfMeasure = p.UnitOfMeasure!.Symbol,
                        Price = p.Price,
                        DiscountPercent = p.DiscountPercent,
                        DepartmentId = p.ProductType!.DepartmentId,
                        DepartmentName = p.ProductType!.Department!.Name,
                        DepartmentSlug = p.ProductType!.Department!.Slug,
                        InStock = p.InventoryItems.Any(i => i.Quantity > 0)
                    }
            )
            .AsAsyncEnumerable();
    }
}