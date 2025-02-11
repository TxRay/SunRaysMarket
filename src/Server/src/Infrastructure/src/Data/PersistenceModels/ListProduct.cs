using SunRaysMarket.Server.Infrastructure.Data.PersistenceModels.Base;

namespace SunRaysMarket.Server.Infrastructure.Data.PersistenceModels;

internal class ListProduct : ModelBase
{
    public int ListId { get; set; }
    public int ProductId { get; set; }

    public List? List { get; set; }
    public Product? Product { get; set; }
}