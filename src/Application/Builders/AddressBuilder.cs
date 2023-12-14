using Application.DomainModels;
using Application.Services;

namespace Application.Builders;

public class AddressBuilder(IAddressService addressService) : IAddressBuilder
{
    private int? _addressId;
    private CreateAddressModel? _address;
    private bool _valueIsSet;

    public void WithNewAddress(CreateAddressModel address)
    {
        if (_valueIsSet)
            throw new InvalidOperationException("Cannot set both new and existing address.");

        _address = address;
        _valueIsSet = true;
    }

    public void WithExistingAddress(int addressId)
    {
        if (_valueIsSet)
            throw new InvalidOperationException("Cannot set both new and existing address.");

        _addressId = addressId;
        _valueIsSet = true;
    }

    public async Task BuildAsync()
    {
        if (!_valueIsSet)
            return;

        if (_addressId is not null)
        {
            AddressId = _addressId.Value;
            return;
        }

        if (_address is null)
            throw new InvalidOperationException("Address must be set.");

        AddressId =
            await addressService.CreateAddressAsync(_address) ?? throw new NullReferenceException();
    }

    public int? AddressId { get; private set; }
}
