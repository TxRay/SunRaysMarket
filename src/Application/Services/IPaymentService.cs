using Application.DomainModels.Payment;

namespace Application.Services;

public interface IPaymentService
{
    public Task<string> AddCard(string token);
    public Task<string> CreateCustomer(CreatePaymentCustomerModel model);
    public Task<ChargeResponseModel> CreateCharge(CreateChargeModel model);
}
