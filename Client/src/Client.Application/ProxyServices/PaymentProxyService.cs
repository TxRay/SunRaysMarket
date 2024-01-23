using System.Net.Http.Json;
using SunRaysMarket.Shared.Core.DomainModels.Payment;
using SunRaysMarket.Shared.Core.DomainModels.Responses;
using SunRaysMarket.Shared.Services.Interfaces;

namespace SunRaysMarket.Client.Application.ProxyServices;

public class PaymentProxyService(HttpClient client) : IPaymentService
{
    public async Task<string> AddCard(CreateCardModel model)
    {
        var response = await client
            .PostAsJsonAsync("api/payments/card", model)
            .ContinueWith(message =>
            {
                message.Result.EnsureSuccessStatusCode();
                return message.Result.Content.ReadFromJsonAsync<AddCardResponse>();
            })
            .Unwrap();

        return response?.CardId ?? throw new InvalidOperationException("CardId was null");
    }

    public Task<string> AddCard(string token)
    {
        throw new NotImplementedException();
    }

    public async Task<string> CreateCustomer(CreatePaymentCustomerModel model)
    {
        var response = await client
            .PostAsJsonAsync("api/payments/customer", model)
            .ContinueWith(message =>
            {
                message.Result.EnsureSuccessStatusCode();
                return message.Result.Content.ReadFromJsonAsync<AddPaymentCustomerResponse>();
            })
            .Unwrap();

        return response?.CustomerPaymentId
            ?? throw new InvalidOperationException("CustomerPaymentId was null");
    }

    public async Task<ChargeResponseModel> CreateCharge(CreateChargeModel model)
    {
        var response = await client
            .PostAsJsonAsync("api/payments/charge", model)
            .ContinueWith(message =>
            {
                message.Result.EnsureSuccessStatusCode();
                return message.Result.Content.ReadFromJsonAsync<ChargeResponseModel>();
            })
            .Unwrap();

        return response ?? throw new InvalidOperationException("ChargeResponseModel was null");
    }
}
