namespace Application.DomainModels;

public class CreateProductTypeModel
{
    public int DepartmentId { get; init; }
    public string Name { get; init; } = default!;
    public string Description { get; init; } = default!;
}
