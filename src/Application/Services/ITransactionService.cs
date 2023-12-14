using Application.DomainModels;
using Application.DomainModels.Checkout;

namespace Application.Services;

public interface ITransactionService
{
    Task CreateTransactionAsync(
        int orderId,
        float amountPaid,
        int billingAddressId,
        string chargeNumber
    );
}
