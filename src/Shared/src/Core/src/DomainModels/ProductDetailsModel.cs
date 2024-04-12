namespace SunRaysMarket.Shared.Core.DomainModels;

public class ProductDetailsModel
{
    public int Id { get; init; }
    public string Name { get; init; } = default!;
    public string Description { get; init; } = default!;
    public string PhotoUrl { get; init; } = default!;
    public float Price { get; init; }
    public float DiscountPercent { get; init; }
    public int DepartmentId { get; init; }
    public string DepartmentName { get; init; } = default!;
    public string DepartmentSlug { get; init; } = default!;
    public int ProductTypeId { get; init; } = default!;
    public string ProductTypeName { get; init; } = default!;
    public string ProductTypeSlug { get; init; } = default!;
    public float Measure { get; init; }
    public string UnitOfMeasure { get; init; } = default!;
    public bool InStock { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }

    public float UnitPrice => Price / Measure;
    public float Discount => Price * DiscountPercent;
    public float SalePrice => Price - Discount;
}