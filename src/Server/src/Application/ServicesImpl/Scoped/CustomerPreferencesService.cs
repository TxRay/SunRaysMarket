using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using SunRaysMarket.Server.Application.UnitOfWork;
using SunRaysMarket.Server.Core.DomainModels;
using SunRaysMarket.Server.Core.Services;

namespace SunRaysMarket.Server.Application.ServicesImpl.Scoped;

public class CustomerPreferencesService(
    IUnitOfWork unitOfWork,
    ICustomerService customerService,
    IHttpContextAccessor accessor
) : ICustomerPreferencesService
{
    public async Task SetCustomerPreferences(UpdateCustomerPreferencesModel model)
    {
        if (!(accessor.HttpContext?.User.IsAuthenticated() ?? false))
            return;

        var customerId = await customerService.GetCurrentCustomerIdAsync(accessor.HttpContext.User);
        if (customerId is null)
            return;

        await unitOfWork.CustomerRepository.SetCustomerPreferences(customerId.Value, model);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task<CustomerPreferences?> GetCustomerPreferencesAsync()
    {
        if (!(accessor.HttpContext?.User.IsAuthenticated() ?? false))
            return null;
        var customerId = await customerService.GetCurrentCustomerIdAsync(accessor.HttpContext.User);

        if (customerId is null)
            return null;

        return await unitOfWork.CustomerRepository.GetCustomerPreferences(customerId.Value);
    }

    public async Task<TPref?> GetCustomerPreference<TPref>(
        Expression<Func<CustomerPreferences, TPref>> get
    )
    {
        if (!(accessor.HttpContext?.User.IsAuthenticated() ?? false))
            return default;
        var customerId = await customerService.GetCurrentCustomerIdAsync(accessor.HttpContext.User);
        var getPreferenceDelegate = get.Compile();

        if (customerId is null)
            return default;
        var preferenceObject = await unitOfWork
            .CustomerRepository
            .GetCustomerPreferences(customerId.Value, cp => getPreferenceDelegate.Invoke(cp));

        return preferenceObject is not null ? (TPref)preferenceObject : default;
    }
}
