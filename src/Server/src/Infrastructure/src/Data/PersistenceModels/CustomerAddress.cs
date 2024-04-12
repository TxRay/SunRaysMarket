using SunRaysMarket.Server.Infrastructure.Data.PersistenceModels.Base;

namespace SunRaysMarket.Server.Infrastructure.Data.PersistenceModels;

internal class CustomerAddress : ModelBase
{
    public int AddressId { get; set; }
    public int CustomerId { get; set; }

    public Address Address { get; set; } = null!;
    public Customer Customer { get; set; } = null!;
}