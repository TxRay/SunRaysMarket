using System.Security.Cryptography;
using Application.DomainModels.Payment;
using Application.Services;
using Stripe;

namespace Infrastructure.Services;

public class StripePaymentService(IStripeClient stripeClient) : IPaymentService
{
    public async Task<string> AddCard(string token)
    {
        var options = new PaymentMethodCreateOptions
        {
            Type = "card",
            Card = new PaymentMethodCardOptions() { Token = token },
        };

        var service = new PaymentMethodService(stripeClient);
        var paymentMethod = await service.CreateAsync(options);

        return paymentMethod.Id;
    }

    public async Task<string> CreateCustomer(CreatePaymentCustomerModel model)
    {
        var options = new CustomerCreateOptions { Name = model.Name, Email = model.Email, };

        var service = new CustomerService(stripeClient);
        var customer = await service.CreateAsync(options);

        return customer.Id;
    }

    public async Task<ChargeResponseModel> CreateCharge(CreateChargeModel model)
    {
        var options = new ChargeCreateOptions
        {
            Amount = model.Amount,
            Currency = model.Currency,
            Source = model.Source,
        };

        var service = new ChargeService(stripeClient);
        var charge = await service.CreateAsync(options);

        var updateOptions = new ChargeUpdateOptions { Customer = model.CustomerPaymentId, };

        charge = await service.UpdateAsync(charge.Id, updateOptions);

        return new ChargeResponseModel
        {
            Id = charge.Id,
            Amount = charge.Amount,
            AmountCaptured = charge.AmountCaptured,
            Currency = charge.Currency,
            Description = charge.Description,
            CustomerId = charge.CustomerId,
        };
    }
}
