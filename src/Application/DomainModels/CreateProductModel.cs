using Application.Structs;

namespace Application.DomainModels;

public class CreateProductModel
{
    public string Name { get; init; } = default!;
    public string Description { get; init; } = default!;
    public string? PhotoUrl { get; set; }
    public int ProductTypeId { get; init; }
    public float Price { get; init; }
    public float DiscountPercent { get; init; }
    public float Measure { get; init; }
    public int UnitOfMeasureId { get; init; }
}
