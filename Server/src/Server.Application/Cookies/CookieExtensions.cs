using System.Text.Json;
using Microsoft.AspNetCore.Http;
using SunRaysMarket.Server.Application.Preferences;

namespace SunRaysMarket.Server.Application.Cookies;

public static class CookieExtensions
{
    private const string Prefix = ".SunRaysMarket.";
    private const string CartKey = Prefix + "Cart";
    private const string PreferencesKey = Prefix + "Preferences";

    public static void SetCartIdCookie(this IResponseCookies cookies, int cartId) =>
        cookies.Append(
            CartKey,
            cartId.ToString(),
            new CookieOptions
            {
                Expires = DateTimeOffset.Now.AddDays(30),
                SameSite = SameSiteMode.Strict
            }
        );

    public static int? GetCartIdCookie(this IRequestCookieCollection cookies) =>
        cookies.TryGetValue(CartKey, out var value) ? int.Parse(value) : null;

    public static void SetPreferencesCookie(
        this IResponseCookies cookies,
        PreferencesModel preferences
    ) =>
        cookies.Append(
            PreferencesKey,
            JsonSerializer.Serialize(preferences),
            new CookieOptions { SameSite = SameSiteMode.Strict }
        );

    public static PreferencesModel GetPreferencesFromCookieOrDefault(
        this IRequestCookieCollection cookies
    ) =>
        cookies.TryGetValue(PreferencesKey, out var value)
            ? JsonSerializer.Deserialize<PreferencesModel>(value) ?? DefaultPreferences.Model
            : DefaultPreferences.Model;
}
