using Microsoft.EntityFrameworkCore.ChangeTracking;
using SunRaysMarket.Server.Application.Repositories;
using SunRaysMarket.Server.Core.DomainModels;

namespace SunRaysMarket.Server.Infrastructure.Repositories;

internal class AddressRepository(ApplicationDbContext dbContext) : IAddressRepository
{
    private EntityEntry? AddressEntry { get; set; }

    public async Task<bool> CreateAddressAsync(CreateAddressModel model)
    {
        var newAddress = new Address
        {
            Street = model.Street,
            City = model.City,
            State = model.State,
            PostalCode = model.PostalCode,
            Country = model.Country
        };

        AddressEntry = await dbContext.Addresses.AddAsync(newAddress);

        return AddressEntry.State == EntityState.Added;
    }

    public async Task<AddressModel?> GetAsync(int id)
    {
        return await dbContext
            .Addresses
            .Where(a => a.Id == id)
            .Select(
                a =>
                    new AddressModel
                    {
                        Id = a.Id,
                        Street = a.Street,
                        City = a.City,
                        State = a.State,
                        PostalCode = a.PostalCode,
                        Country = a.Country
                    }
            )
            .FirstOrDefaultAsync();
    }

    public async Task<bool> UpdateAsync(UpdateAddressModel model)
    {
        var address = await dbContext.Addresses.FindAsync(model.Id);

        if (address is null)
            return false;

        address.Street = model.Street ?? address.Street;
        address.City = model.City ?? address.City;
        address.State = model.State ?? address.State;
        address.PostalCode = model.PostalCode ?? address.PostalCode;
        address.Country = model.Country ?? address.Country;

        AddressEntry = dbContext.Addresses.Update(address);

        return AddressEntry.State == EntityState.Modified;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var address = await dbContext.Addresses.FindAsync(id);

        if (address is null)
            return false;

        AddressEntry = dbContext.Addresses.Remove(address);

        return AddressEntry.State == EntityState.Deleted;
    }

    public AddressModel? GetPersisted()
    {
        if (AddressEntry?.State is not EntityState.Unchanged || !AddressEntry.IsKeySet)
            return null;

        return AddressEntry.Entity is Address address
            ? new AddressModel
            {
                Id = address.Id,
                Street = address.Street,
                City = address.City,
                State = address.State,
                PostalCode = address.PostalCode,
                Country = address.Country
            }
            : null;
    }

    public int? GetPersistedId()
    {
        if (AddressEntry?.State is not EntityState.Unchanged || !AddressEntry.IsKeySet)
            return null;

        return AddressEntry.Entity is Address address ? address.Id : null;
    }
}
