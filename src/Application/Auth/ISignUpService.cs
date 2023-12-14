using System.Linq.Expressions;
using Application.DomainModels;
using Application.Results;

namespace Application.Auth;

public interface ISignUpService
{
    Task<AuthResult> CustomerSignUpAsync(SignUpModel signUpModel, IEnumerable<Role> roles);
}
