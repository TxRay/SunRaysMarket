using System.ComponentModel.DataAnnotations;

namespace Application.DomainModels;

public class LoginModel
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}