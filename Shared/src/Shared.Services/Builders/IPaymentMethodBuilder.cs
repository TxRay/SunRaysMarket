using SunRaysMarket.Shared.Core.Payment;

namespace SunRaysMarket.Shared.Services.Builders;

public interface IPaymentMethodBuilder
{
    void WithCreditCard(CardDefinition card);
}
