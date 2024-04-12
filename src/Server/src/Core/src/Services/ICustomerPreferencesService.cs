using System.Linq.Expressions;
using SunRaysMarket.Server.Core.DomainModels;

namespace SunRaysMarket.Server.Core.Services;

public interface ICustomerPreferencesService
{
    Task SetCustomerPreferences(UpdateCustomerPreferencesModel model);
    Task<CustomerPreferences?> GetCustomerPreferencesAsync();
    Task<TPref?> GetCustomerPreference<TPref>(Expression<Func<CustomerPreferences, TPref>> get);
}