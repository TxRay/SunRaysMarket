using Infrastructure.Data.PersistenceModels.Base;

namespace Infrastructure.Data.PersistenceModels;

internal class Customer : BaseModel
{
    public int UserId { get; set; }
    public int? CartId { get; set; }

    public User? User { get; set; }
    public Cart? Cart { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
