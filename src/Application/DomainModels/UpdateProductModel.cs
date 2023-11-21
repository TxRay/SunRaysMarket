namespace Application.DomainModels;

public class UpdateProductModel
{
    public int Id { get; init; }
    public string Name { get; init; } = default!;
    public string Description { get; init; } = default!;
    public string PhotoUrl { get; init; } = default!;
    public int ProductTypeId { get; init; }
    public float Price { get; init; }
    public float DiscountPercent { get; init; }
    public float Measure { get; init; }
    public int UnitOfMeasureId { get; init; }
}
