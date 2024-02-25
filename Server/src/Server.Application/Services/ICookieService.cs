using System.Linq.Expressions;
using SunRaysMarket.Server.Application.Preferences;

namespace SunRaysMarket.Server.Application.Services;

public interface ICookieService
{
    int? CartId { get; set; }
    CustomerPreferences? Preferences { get; set; }
    bool WasCookieUpdated(Expression<Func<ICookieService, object>> selector);
    void DeleteCookie(Expression<Func<ICookieService, object>> selector);
    void Reset();
}