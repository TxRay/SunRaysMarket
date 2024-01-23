using SunRaysMarket.Shared.Core.Payment;
using SunRaysMarket.Shared.Services.Interfaces;

namespace SunRaysMarket.Shared.Services.Builders;

public class PaymentMethodBuilder(IPaymentService paymentService) : IPaymentMethodBuilder
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
        {
            PaymentMethodId = TestCards.GetCardPmTokenOrDefault(_card);
        }
    }
}
