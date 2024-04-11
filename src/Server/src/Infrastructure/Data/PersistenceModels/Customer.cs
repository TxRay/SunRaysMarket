using SunRaysMarket.Server.Infrastructure.Data.PersistenceModels.Base;

namespace SunRaysMarket.Server.Infrastructure.Data.PersistenceModels;

internal class Customer : ModelBase
{
    public int UserId { get; set; }
    public int? CartId { get; set; }
    public int? PreferredStoreId { get; set; }
    public string? PaymentId { get; set; }
    public User? User { get; set; }
    public Cart? Cart { get; set; }

    public Store? PreferredStore { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    public virtual ICollection<Address> Addresses { get; set; } = [];
}
