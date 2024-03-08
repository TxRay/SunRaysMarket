namespace SunRaysMarket.Shared.Core.Payment;

public static class TestCards
{
    public static readonly IEnumerable<TestCard> CardList =
    [
        new TestCard
        {
            Id = "tok_visa",
            Title = "Visa",
            Definition = new CardDefinition("Jack Bauer", "4242424242424242", "04/29", "123")
        },
        new TestCard
        {
            Id = "tok_visa_debit",
            Title = "Visa Debit",
            Definition = new CardDefinition("Frodo Baggins", "4000056655665556", "03/28", "456")
        },
        new TestCard
        {
            Id = "tok_mastercard",
            Title = "Mastercard",
            Definition = new CardDefinition("Darth Vader", "5555555555554444", "05/30", "789")
        }
    ];

    public static string? GetCardPmTokenOrDefault(CardDefinition card)
    {
        return CardList.FirstOrDefault(tc => tc.Definition == card)?.Id;
    }
}
