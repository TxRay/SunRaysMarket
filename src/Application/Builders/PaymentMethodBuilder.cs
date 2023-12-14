using Application.DomainModels.Payment;
using Application.Payment;
using Application.Services;

namespace Application.Builders;

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
