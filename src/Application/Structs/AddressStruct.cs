namespace Application.Structs;

public struct AddressStruct
{
    public string Street { get; init; }
    public string City { get; init; }
    public string PostalCode { get; init; }
    public string State { get; init; }
    public string Country { get; init; }

    public AddressStruct(
        string street,
        string city,
        string postalCode,
        string state,
        string country
    )
    {
        Street = street;
        City = city;
        PostalCode = postalCode;
        State = state;
        Country = country;
    }

    public string FormatAddress(string countryCode)
    {
        return countryCode switch
        {
            "US" => $"{Street}, {City}, {State} {PostalCode}",
            "CA" => $"{Street}, {City}, {State} {PostalCode}",
            "MX" => $"{Street}, {City}, {State} {PostalCode}",
            _ => $"{Street}, {City}, {State} {PostalCode}, {Country}"
        };
    }
}
