using Application.Structs;

namespace Application.DomainModels;

public class ProductListModel
{
    public int Id { get; init; }
    public string Name { get; init; } = default!;
    public string Slug { get; init; } = default!;
    public string PhotoUrl { get; init; } = default!;
    public float Price { get; init; }
    public float DiscountPercent { get; init; }

    //public int ProductTypeId { get; init; } = default!;
    //public string ProductTypeName { get; init; } = default!;
    public int ProductTypeSlug { get; init; } = default!;
    public int DepartmentId { get; init; }
    public string DepartmentName { get; init; } = default!;
    public string DepartmentSlug { get; init; } = default!;
    public bool InStock { get; init; }

    public float Discount => Price * DiscountPercent;
    public float SalePrice => Price - Discount;
}
