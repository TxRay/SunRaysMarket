using Infrastructure.Data.PersistenceModels.Base;

namespace Infrastructure.Data.PersistenceModels;

internal class TimeSlot : BaseModel
{
    public int StoreId { get; set; }
    public int TimeSlotDefinitionId { get; set; }
    public DateOnly Date { get; set; }

    public int Capacity { get; set; }

    public int Filled { get; set; }

    public Store? Store { get; set; }
    public TimeSlotDefinition? TimeSlotDefinition { get; set; }
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
