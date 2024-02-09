using SunRaysMarket.Server.Infrastructure.Data.PersistenceModels.Base;

namespace SunRaysMarket.Server.Infrastructure.Data.PersistenceModels;

internal class Store : TimeStampModelBase
{
    public string LocationName { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string EmailAddress { get; set; } = default!;
    public string ManagerName { get; set; } = default!;
    public int? AddressId { get; set; }

    public Address? Address { get; set; }

    public virtual ICollection<ProductInventory> InventoryItems { get; set; } =
        new List<ProductInventory>();
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    public virtual ICollection<TimeSlot> TimeSlots { get; set; } = new List<TimeSlot>();
}
