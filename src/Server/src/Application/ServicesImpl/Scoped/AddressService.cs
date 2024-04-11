using SunRaysMarket.Server.Application.UnitOfWork;
using SunRaysMarket.Shared.Core.Services;

namespace SunRaysMarket.Server.Application.ServicesImpl.Scoped;

internal class AddressService(IUnitOfWork unitOfWork) : IAddressService
{
    public async Task<int?> CreateAddressAsync(CreateAddressModel model)
    {
        if (!await unitOfWork.AddressRepository.CreateAddressAsync(model))
            return null;

        await unitOfWork.SaveChangesAsync();

        return unitOfWork.AddressRepository.GetPersistedId() ?? throw new NullReferenceException();
    }

    public Task<AddressModel?> GetAddressAsync(int addressId)
    {
        return unitOfWork.AddressRepository.GetAsync(addressId);
    }
}
