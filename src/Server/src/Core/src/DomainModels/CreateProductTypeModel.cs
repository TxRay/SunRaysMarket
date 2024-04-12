namespace SunRaysMarket.Server.Core.DomainModels;

public class CreateProductTypeModel
{
    public int DepartmentId { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
}