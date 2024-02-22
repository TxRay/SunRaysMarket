namespace SunRaysMarket.Shared.Core.Payment;

public static class TestCards
{
    public static readonly IEnumerable<TestCard> CardList =
    [
        new TestCard
        {
            Id = "tok_visa",
            Title = "Visa",
            Definition = new CardDefinition(
                Name: "Jack Bauer",
                Number: "4242424242424242",
                Expiry: "04/29",
                Cvc: "123"
            )
        },
        new TestCard
        {
            Id = "tok_visa_debit",
            Title = "Visa Debit",
            Definition = new CardDefinition(
                Name: "Frodo Baggins",
                Number: "4000056655665556",
                Expiry: "03/28",
                Cvc: "456"
            )
        },
        new TestCard
        {
            Id = "tok_mastercard",
            Title = "Mastercard",
            Definition = new CardDefinition(
                Name: "Darth Vader",
                Number: "5555555555554444",
                Expiry: "05/30",
                Cvc: "789"
            )
        },
    ];

    public static string? GetCardPmTokenOrDefault(CardDefinition card) =>
        CardList.FirstOrDefault(tc => tc.Definition == card)?.Id;
}