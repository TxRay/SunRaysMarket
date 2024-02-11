using Microsoft.AspNetCore.Http;
using SunRaysMarket.Server.Application.UnitOfWork;
using SunRaysMarket.Shared.Services.Interfaces;

namespace SunRaysMarket.Server.Application.Services;

public class CustomerPreferencesService(
    IHttpContextAccessor accessor,
    ICustomerService customerService,
    IUnitOfWork unitOfWork)
    : ICustomerPreferencesService
{
    public async Task SetPreferredStoreAsync(int storeId)
    {
        var user = accessor.HttpContext?.User;

        if (user is null)
            return;

        var customerId = await customerService.GetCurrentCustomerIdAsync(user)
                         ?? throw new NullReferenceException("No customer data was found for the current model");

        await unitOfWork.CustomerRepository.SetCustomerPreferences(
            customerId,
            new UpdateCustomerPreferencesModel
            {
                PreferredStoreId = storeId
            }
        );
        await unitOfWork.SaveChangesAsync();
    }

    public async Task<int?> GetPreferredStoreAsync()
    {
        var user = accessor.HttpContext?.User;

        if (user is null)
            return null;
        
        var customerId = await customerService.GetCurrentCustomerIdAsync(user)
                         ?? throw new NullReferenceException("No customer data was found for the current model");


        var preferences = await unitOfWork.CustomerRepository.GetCustomerPreferences(
            customerId,
            model => new { model.PreferredStoreId }
        );

        return preferences?.GetType().GetProperty("PreferredStoreId")?.GetValue(preferences) as int?;
    }
}