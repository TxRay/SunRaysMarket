namespace Infrastructure.Data.PersistenceModels.Base;

internal class TimeStampBaseModel : BaseModel
{
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
