namespace SunRaysMarket.Server.Infrastructure.Data.PersistenceModels.Base;

internal class TimeStampModelBase : ModelBase
{
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}