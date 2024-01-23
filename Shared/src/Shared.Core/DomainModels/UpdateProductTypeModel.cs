namespace SunRaysMarket.Shared.Core.DomainModels;

public class UpdateProductTypeModel
{
    public int Id { get; init; }
    public int DepartmentId { get; init; }
    public string Name { get; init; } = default!;
}