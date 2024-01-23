namespace SunRaysMarket.Shared.Core.DomainModels;

public class CreateProductModel
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string? PhotoUrl { get; set; }
    public int ProductTypeId { get; set; }
    public float Price { get; set; }
    public float DiscountPercent { get; set; }
    public float Measure { get; set; }
    public int UnitOfMeasureId { get; set; }

    public float DiscountDecimal => DiscountPercent / 100;
}