using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SunRaysMarket.Server.Application.Services;
using SunRaysMarket.Server.Application.UnitOfWork;
using SunRaysMarket.Shared.Services.Interfaces;

namespace SunRaysMarket.Server.Application.ServicesImpl.Scoped;

public class CustomerAddressService(
    ICustomerService customerService,
    ILogger<CustomerAddressService> logger,
    IHttpContextAccessor contextAccessor,
    IUnitOfWork unitOfWork
) : ICustomerAddressService
{
    public async Task<IEnumerable<AddressModel>> GetAddressesAsync()
    {
        var customerId = await customerService.GetCurrentCustomerIdAsync(Context.User);

        if (customerId is null)
        {
            logger.LogWarning("There is no currently authenticated customer.");
            return [];
        }

        return await unitOfWork.CustomerRepository.GetCustomerAddresses(customerId.Value);
    }

    public async Task<int?> AddAddress(CreateAddressModel model)
    {
        var customerId = await customerService.GetCurrentCustomerIdAsync(Context.User);

        if (customerId is null)
        {
            logger.LogWarning("There is no currently authenticated customer.");
            return null;
        }

        if (!(await unitOfWork.AddressRepository.CreateAddressAsync(model)))
        {
            logger.LogWarning("The requested address could not be added to the database.");
            return null;
        }

        await unitOfWork.SaveChangesAsync();
        var addressId = unitOfWork.AddressRepository.GetPersistedId();

        if (addressId is null)
        {
            logger.LogWarning("The requested address could not be added to the database.");
            return null;
        }

        await unitOfWork
            .CustomerRepository
            .AddCustomerAddressAsync(customerId.Value, addressId.Value);
        await unitOfWork.SaveChangesAsync();

        return addressId;
    }

    public async Task RemoveAddress(int addressId)
    {
        var customerId = await customerService.GetCurrentCustomerIdAsync(Context.User);

        if (customerId is null)
        {
            logger.LogWarning("There is no currently authenticated customer.");
            return;
        }

        await unitOfWork.CustomerRepository.RemoveCustomerAddressAsync(customerId.Value, addressId);
    }

    private HttpContext Context =>
        contextAccessor.HttpContext
        ?? throw new InvalidOperationException("No HttpContext is available.");
}
