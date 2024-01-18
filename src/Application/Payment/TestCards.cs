namespace Application.Payment;

public static class TestCards
{
    public static readonly Dictionary<CardDefinition, string> CardMap =
        new()
        {
            {
                new CardDefinition(
                    Name: "Jack Bauer",
                    Number: "4242424242424242",
                    Expiry: "04/29",
                    Cvc: "123"
                ),
                "tok_visa"
            },
            {
                new CardDefinition(
                    Name: "Frodo Baggins",
                    Number: "4000056655665556",
                    Expiry: "03/28",
                    Cvc: "456"
                ),
                "tok_visa_debit"
            },
            {
                new CardDefinition(
                    Name: "Darth Vader",
                    Number: "5555555555554444",
                    Expiry: "05/30",
                    Cvc: "789"
                ),
                "tok_mastercard"
            }
        };

    public static string? GetCardPmTokenOrDefault(CardDefinition card) =>
        TestCards.CardMap.GetValueOrDefault(card);
  
}
