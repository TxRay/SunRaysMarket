using Application.DomainModels;
using Infrastructure.Data.PersistenceModels.Base;

namespace Infrastructure.Data.PersistenceModels;

internal class OrderSubstitution : TimeStampBaseModel
{
    public int OrderLineId { get; set; }
    public int OriginalItemId { get; set; }
    public int SubstituteItemId { get; set; }

    public Product? OriginalItem { get; set; }
    public Product? SubstituteItem { get; set; }
    public OrderLine? OrderLine { get; set; }
}
