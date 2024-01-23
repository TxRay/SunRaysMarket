using SunRaysMarket.Server.Application.Repositories;
using SunRaysMarket.Server.Infrastructure.Data;
using SunRaysMarket.Server.Infrastructure.Data.PersistenceModels;
using SunRaysMarket.Shared.Core.DomainModels;

namespace SunRaysMarket.Server.Infrastructure.Repositories;

internal class TransactionRepository(ApplicationDbContext dbContext) : ITransactionRepository
{
    public async Task AddTransactionAsync(CreateTransactionModel model)
    {
        var newTransaction = new Transaction
        {
            OrderId = model.OrderId,
            BillingAddressId = model.BillingAddressId,
            Status = model.Status,
            ChargeNumber = model.ChargeNumber,
            PaymentMethod = model.PaymentMethod!,
            AmountPaid = model.AmountPaid
        };

        await dbContext.Transactions.AddAsync(newTransaction);
    }

    public async Task<IEnumerable<TransactionDetailsModel>> GetTransactionsAsync(int customerId) =>
        await dbContext
            .Transactions
            .Include(t => t.Order)
            .ThenInclude(o => o!.Customer)
            .ThenInclude(c => c!.User)
            .Include(t => t.Order)
            .Where(t => t.Order!.CustomerId == customerId)
            .Select(
                t =>
                    new TransactionDetailsModel
                    {
                        Id = t.Id,
                        CustomerId = t.Order!.CustomerId,
                        TransactionNumber = t.TransactionNumber,
                        OrderNumber = t.Order!.OrderNumber,
                        CustomerName =
                            $"{t.Order!.Customer!.User!.FirstName} {t.Order!.Customer!.User!.LastName}",
                        OrderId = t.OrderId,
                        Status = t.Status,
                        PaymentMethod = t.PaymentMethod,
                        AmountPaid = t.AmountPaid,
                    }
            )
            .ToListAsync();

    public async Task<TransactionDetailsModel?> GetTransactionAsync(long transactionNumber) =>
        await dbContext
            .Transactions
            .Include(t => t.Order)
            .ThenInclude(t => t!.Customer)
            .ThenInclude(c => c!.User)
            .Where(t => t.TransactionNumber == transactionNumber)
            .Select(
                t =>
                    new TransactionDetailsModel
                    {
                        Id = t.Id,
                        CustomerId = t.Order!.CustomerId,
                        TransactionNumber = t.TransactionNumber,
                        OrderNumber = t.Order!.OrderNumber,
                        CustomerName =
                            $"{t.Order!.Customer!.User!.FirstName} {t.Order!.Customer!.User!.LastName}",
                        OrderId = t.OrderId,
                        Status = t.Status,
                        PaymentMethod = t.PaymentMethod,
                        AmountPaid = t.AmountPaid,
                    }
            )
            .FirstOrDefaultAsync();

    public async Task<TransactionDetailsModel?> GetTransactionAsync(int transactionId) =>
        await dbContext
            .Transactions
            .Include(t => t.Order)
            .ThenInclude(o => o!.Customer)
            .ThenInclude(c => c!.User)
            .Where(t => t.Id == transactionId)
            .Select(
                t =>
                    new TransactionDetailsModel
                    {
                        Id = t.Id,
                        CustomerId = t.Order!.CustomerId,
                        TransactionNumber = t.TransactionNumber,
                        OrderNumber = t.Order!.OrderNumber,
                        CustomerName =
                            $"{t.Order!.Customer!.User!.FirstName} {t.Order!.Customer!.User!.LastName}",
                        OrderId = t.OrderId,
                        Status = t.Status,
                        PaymentMethod = t.PaymentMethod,
                        AmountPaid = t.AmountPaid,
                    }
            )
            .FirstOrDefaultAsync();

    public async Task<bool> UpdateTransactionMethodAsync(int transactionId, string method)
    {
        var transaction = await dbContext.Transactions.FindAsync(transactionId);

        if (transaction is null)
            return false;

        transaction.PaymentMethod = method;

        return true;
    }

    public async Task<bool> UpdateTransactionStatusAsync(int transactionId, int status)
    {
        var transaction = await dbContext.Transactions.FindAsync(transactionId);

        if (transaction is null)
            return false;

        transaction.Status = status;

        return true;
    }

    public async Task<bool> UpdateTransactionAmountAsync(int transactionId, float amount)
    {
        var transaction = await dbContext.Transactions.FindAsync(transactionId);

        if (transaction is null)
            return false;

        transaction.AmountPaid = amount;

        return true;
    }

    public async Task DeleteTransactionAsync(int transactionId)
    {
        var transaction = await dbContext.Transactions.FindAsync(transactionId);

        if (transaction is null)
            return;

        dbContext.Transactions.Remove(transaction);
    }
}
