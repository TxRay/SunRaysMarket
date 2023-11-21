using Application.DomainModels;
using Application.Repositories;

namespace Infrastructure.Repositories;

internal class TransactionRepository : ITransactionRepository
{
    public Task AddTransactionAsync(int customerId, CreateTransactionModel model)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TransactionDetailsModel>> GetTransactionsAsync(int customerId)
    {
        throw new NotImplementedException();
    }

    public Task<TransactionDetailsModel> GetTransactionAsync(int customerId, int transactionId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateTransactionMethodAsync(int transactionId, string method)
    {
        throw new NotImplementedException();
    }

    public Task UpdateTransactionStatusAsync(int transactionId, int status)
    {
        throw new NotImplementedException();
    }

    public Task UpdateTransactionAmountAsync(int transactionId, float amount)
    {
        throw new NotImplementedException();
    }

    public Task DeleteTransactionAsync(int transactionId)
    {
        throw new NotImplementedException();
    }
}
