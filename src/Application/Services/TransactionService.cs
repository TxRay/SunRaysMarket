using Application.DomainModels;
using Application.DomainModels.Checkout;
using Application.UnitOfWork;

namespace Application.Services;

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
