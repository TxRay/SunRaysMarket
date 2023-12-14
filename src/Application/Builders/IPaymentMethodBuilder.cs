using Application.DomainModels.Payment;
using Application.Payment;

namespace Application.Builders;

public interface IPaymentMethodBuilder
{
    void WithCreditCard(CardDefinition card);
}
