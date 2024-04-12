using SunRaysMarket.Server.Core.DomainModels.Payment;

namespace SunRaysMarket.Server.Core.Services;

public interface IPaymentService
{
    public Task<string> AddCard(string token);
    public Task<string> CreateCustomer(CreatePaymentCustomerModel model);
    public Task<ChargeResponseModel> CreateCharge(CreateChargeModel model);
}