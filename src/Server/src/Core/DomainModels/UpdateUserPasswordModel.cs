namespace SunRaysMarket.Server.Core.DomainModels;

public class UpdateUserPasswordModel
{
    public string NewPassword { get; set; } = string.Empty;
    public string ConfirmNewPassword { get; set; } = string.Empty;
}
