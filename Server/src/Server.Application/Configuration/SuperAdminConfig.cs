namespace SunRaysMarket.Server.Application.Configuration;

#nullable disable

public class SuperAdminConfig
{
    public const string GroupName = "SuperAdminUser";
    
    public string Email { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}