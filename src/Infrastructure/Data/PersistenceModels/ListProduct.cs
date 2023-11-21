using Infrastructure.Data.PersistenceModels.Base;

namespace Infrastructure.Data.PersistenceModels;

internal class ListProduct : BaseModel
{
    public int ListId { get; set; }
    public int ProductId { get; set; }

    public List? List { get; set; }
    public Product? Product { get; set; }
}
