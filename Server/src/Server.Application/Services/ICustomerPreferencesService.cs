using System.Linq.Expressions;

namespace SunRaysMarket.Server.Application.Services;

public interface ICustomerPreferencesService
{
    Task SetCustomerPreferences(UpdateCustomerPreferencesModel model);
    Task<CustomerPreferences?> GetCustomerPreferencesAsync();
    Task<TPref?> GetCustomerPreference<TPref>(Expression<Func<CustomerPreferences, TPref>> get);
}