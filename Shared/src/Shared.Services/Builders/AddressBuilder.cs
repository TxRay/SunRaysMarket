using SunRaysMarket.Shared.Services.Interfaces;

namespace SunRaysMarket.Shared.Services.Builders;

public class AddressBuilder(IAddressService addressService) : IAddressBuilder
{
    private CreateAddressModel? _address;
    private int? _addressId;
    private bool _valueIsSet;

    public int? AddressId { get; private set; }

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
}
