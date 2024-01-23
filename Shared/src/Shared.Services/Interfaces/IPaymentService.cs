using SunRaysMarket.Shared.Core.DomainModels.Payment;

namespace SunRaysMarket.Shared.Services.Interfaces;

public interface IPaymentService
{
    public Task<string> AddCard(string token);
    public Task<string> CreateCustomer(CreatePaymentCustomerModel model);
    public Task<ChargeResponseModel> CreateCharge(CreateChargeModel model);
}
