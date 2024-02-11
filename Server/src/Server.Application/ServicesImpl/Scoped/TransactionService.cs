using SunRaysMarket.Server.Application.Services;
using SunRaysMarket.Server.Application.UnitOfWork;

namespace SunRaysMarket.Server.Application.ServicesImpl.Scoped;

public class TransactionService(IUnitOfWork unitOfWork) : ITransactionService
{
    public async Task CreateTransactionAsync(
        int orderId,
        float amountPaid,
        int billingAddressId,
        string chargeNumber
    )
    {
        var newTransaction = new CreateTransactionModel
        {
            OrderId = orderId,
            BillingAddressId = billingAddressId,
            ChargeNumber = chargeNumber,
            Status = 0,
            PaymentMethod = "Credit Card",
            AmountPaid = amountPaid
        };

        await unitOfWork.TransactionRepository.AddTransactionAsync(newTransaction);
        await unitOfWork.SaveChangesAsync();
    }
}
