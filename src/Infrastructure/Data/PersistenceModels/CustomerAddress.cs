using Infrastructure.Data.PersistenceModels.Base;

namespace Infrastructure.Data.PersistenceModels;

internal class CustomerAddress : BaseModel
{
    public int AddressId { get; set; }
    public int CustomerId { get; set; }

    public Address Address { get; set; } = null!;
    public Customer Customer { get; set; } = null!;
}