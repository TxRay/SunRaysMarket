using System.Linq.Expressions;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using SunRaysMarket.Server.Core.DomainModels;
using SunRaysMarket.Server.Core.Services;
using SunRaysMarket.Shared.Extensions.Visitors;

namespace SunRaysMarket.Server.Infrastructure.ServicesImpl.Singleton;

internal class CookieService(IHttpContextAccessor accessor) : ICookieService
{
    private const string Prefix = ".SunRaysMarket.";
    private const string CartKey = Prefix + "Cart";
    private const string PreferencesKey = Prefix + "Preferences";
    private List<string> _updatedCookieProperties = [];

    public int? CartId
    {
        get => GetCookie<int?>(CartKey);
        set
        {
            ArgumentNullException.ThrowIfNull(value);

            SetCookie(
                CartKey,
                value,
                new CookieOptions
                {
                    Expires = DateTimeOffset.Now.AddDays(2),
                    SameSite = SameSiteMode.Strict,
                    HttpOnly = true,
                    Secure = true
                }
            );

            _updatedCookieProperties.Add(nameof(CartId));
        }
    }

    public CustomerPreferences? Preferences
    {
        get => GetCookie<CustomerPreferences>(PreferencesKey);
        set
        {
            ArgumentNullException.ThrowIfNull(value);

            SetCookie(
                PreferencesKey,
                value,
                new CookieOptions
                {
                    Expires = DateTimeOffset.Now.AddDays(30),
                    SameSite = SameSiteMode.Strict,
                    HttpOnly = true,
                    Secure = true
                }
            );

            _updatedCookieProperties.Add(nameof(Preferences));
        }
    }

    public bool WasCookieUpdated(Expression<Func<ICookieService, object>> selector)
    {
        var visitor = new PropertyNameExtractingVisitor();
        visitor.Visit(selector);

        return _updatedCookieProperties.Contains(visitor.PropertyName);
    }

    public void DeleteCookie(Expression<Func<ICookieService, object>> selector)
    {
        var visitor = new PropertyNameExtractingVisitor();
        visitor.Visit(selector);

        var deleteKey = visitor.PropertyName switch
        {
            "CartId" => CartKey,
            "Preferences" => PreferencesKey,
            _ => string.Empty
        };

        DeleteCookie(deleteKey);
    }

    public void Reset()
    {
        _updatedCookieProperties = [];
    }

    private void SetCookie(string key, object value, CookieOptions options)
    {
        accessor
            .HttpContext
            ?.Response
            .Cookies
            .Append(key, JsonSerializer.Serialize(value), options);
    }

    private void DeleteCookie(string key)
    {
        if (string.IsNullOrEmpty(key))
            return;

        accessor.HttpContext?.Response.Cookies.Delete(key);
    }

    private TValue? GetCookie<TValue>(string key)
    {
        return accessor.HttpContext?.Request.Cookies.TryGetValue(key, out var value) ?? false
            ? JsonSerializer.Deserialize<TValue>(value)
            : default;
    }
}