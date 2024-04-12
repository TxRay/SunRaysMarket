using SunRaysMarket.Shared.Core.Payment;

namespace SunRaysMarket.Shared.Core.Builders;

public interface IPaymentMethodBuilder
{
    void WithCreditCard(CardDefinition card);
}