using SunRaysMarket.Server.Application.UnitOfWork;
using SunRaysMarket.Server.Core.DomainModels;
using SunRaysMarket.Server.Core.Services;

namespace SunRaysMarket.Server.Application.ServicesImpl.Scoped;

public class TransactionService(IUnitOfWork unitOfWork) : ITransactionService
{
    public async Task CreateTransactionAsync(int orderId, float amountPaid, string chargeNumber)
    {
        var newTransaction = new CreateTransactionModel
        {
            OrderId = orderId,
            ChargeNumber = chargeNumber,
            Status = 0,
            PaymentMethod = "Credit Card",
            AmountPaid = amountPaid
        };

        await unitOfWork.TransactionRepository.AddTransactionAsync(newTransaction);
        await unitOfWork.SaveChangesAsync();
    }
}