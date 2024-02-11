using SunRaysMarket.Server.Application.Preferences;

namespace SunRaysMarket.Server.Application.Services;

public interface ICookieService
{
    void SetCartIdCookie(int cartId);
    int? GetCartIdCookie();
    void SetPreferencesCookie(PreferencesModel preferences);
    PreferencesModel GetPreferencesFromCookieOrDefault();
}