using System.Text.Json;
using Microsoft.AspNetCore.Http;
using SunRaysMarket.Server.Application.Preferences;
using SunRaysMarket.Server.Application.Services;

namespace SunRaysMarket.Server.Infrastructure.ServicesImpl.Transient;

internal class CookieService(IHttpContextAccessor accessor) : ICookieService
{
    private const string Prefix = ".SunRaysMarket.";
    private const string CartKey = Prefix + "Cart";
    private const string PreferencesKey = Prefix + "Preferences";

    public void SetCartIdCookie(int cartId)
        => SetCookie(CartKey, cartId,
            new CookieOptions
            {
                Expires = DateTimeOffset.Now.AddDays(2),
                SameSite = SameSiteMode.Strict,
                HttpOnly = true,
                Secure = true
            }
        );
    
    public int? GetCartIdCookie()
        => GetCookie<int?>(CartKey);

    public void SetPreferencesCookie(PreferencesModel preferences)
        => SetCookie(PreferencesKey, preferences, new CookieOptions
        {
            Expires = DateTimeOffset.Now.AddDays(30),
            SameSite = SameSiteMode.Strict,
            HttpOnly = true,
            Secure = true
        });

    public PreferencesModel GetPreferencesFromCookieOrDefault()
        => GetCookie<PreferencesModel>(PreferencesKey) ?? DefaultPreferences.Model;

    private void SetCookie(string key, object value, CookieOptions options)
        => accessor.HttpContext?.Response.Cookies.Append(key, JsonSerializer.Serialize(value), options);

    private TValue? GetCookie<TValue>(string key)
        => accessor.HttpContext?.Request.Cookies.TryGetValue(key, out var value) ?? false
            ? JsonSerializer.Deserialize<TValue>(value)
            : default;
}