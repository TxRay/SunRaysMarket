namespace SunRaysMarket.Server.Application.Enums;

public enum Role
{
    Admin,
    Customer,
    Employee,
    Manager

    /*public static readonly string[] Roles =
    {
        "SuperAdmin",
        "Admin",
        "Customer",
        "Employee",
        "Manager"
    };*/
}

public static class RoleExtensions
{
    public static string ToRoleName(this Role role) =>
        role switch
        {
            Role.Admin => "Admin",
            Role.Customer => "Customer",
            Role.Employee => "Employee",
            Role.Manager => "Manager",
            _ => throw new ArgumentOutOfRangeException(nameof(role), role, null)
        };
}
