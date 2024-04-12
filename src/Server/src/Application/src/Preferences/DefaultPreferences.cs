using SunRaysMarket.Server.Core.DomainModels;

namespace SunRaysMarket.Server.Application.Preferences;

public static class DefaultPreferences
{
    public static readonly CustomerPreferences Model = new() { PreferredStoreId = 2 };
}