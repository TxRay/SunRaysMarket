using Application.DomainModels;
using Application.Results;

namespace Application.Auth;

public interface ILoginService
{
    public Task<AuthResult> LoginAsync(LoginModel loginModel);
}