using SunRaysMarket.Shared.Core.Payment;
using SunRaysMarket.Shared.Core.Services;

namespace SunRaysMarket.Shared.Core.Builders;

public class PaymentMethodBuilder : IPaymentMethodBuilder
{
    private CardDefinition? _card;
    public string? PaymentMethodId { get; private set; }

    public void WithCreditCard(CardDefinition card)
    {
        _card = card;
    }

    public void Build()
    {
        if (_card is not null)
            PaymentMethodId = TestCards.GetCardPmTokenOrDefault(_card);
    }
}
