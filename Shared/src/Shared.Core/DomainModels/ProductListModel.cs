namespace SunRaysMarket.Shared.Core.DomainModels;

public class ProductListModel
{
    public int Id { get; init; }
    public string Name { get; init; } = default!;
    public string Slug { get; init; } = default!;
    public string PhotoUrl { get; init; } = default!;
    public float Price { get; init; }

    public float Measure { get; init; }

    public string UnitOfMeasure { get; init; } = default!;
    public float DiscountPercent { get; init; }

    public int ProductTypeSlug { get; init; } = default!;
    public int DepartmentId { get; init; }
    public string DepartmentName { get; init; } = default!;
    public string DepartmentSlug { get; init; } = default!;
    public bool InStock { get; init; }

    public float Discount => Price * DiscountPercent;
    public float SalePrice => Price - Discount;

    public float UnitPrice => Price / Measure;
}