using Application.DomainModels;
using Application.UnitOfWork;

namespace Application.Services;

internal class AddressService(IUnitOfWork unitOfWork) : IAddressService
{
    public async Task<int?> CreateAddressAsync(CreateAddressModel model)
    {
        if (!await unitOfWork.AddressRepository.CreateAddressAsync(model))
            return null;

        await unitOfWork.SaveChangesAsync();

        return unitOfWork.AddressRepository.GetPersistedId() ?? throw new NullReferenceException();
    }

    public Task<AddressModel?> GetAddressAsync(int addressId) =>
        unitOfWork.AddressRepository.GetAsync(addressId);
}
