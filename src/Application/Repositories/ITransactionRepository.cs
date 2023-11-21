using Application.DomainModels;

namespace Application.Repositories;

public interface ITransactionRepository
{
    Task AddTransactionAsync(int customerId, CreateTransactionModel model);
    Task<IEnumerable<TransactionDetailsModel>> GetTransactionsAsync(int customerId);
    Task<TransactionDetailsModel> GetTransactionAsync(int customerId, int transactionId);
    Task UpdateTransactionMethodAsync(int transactionId, string method);
    Task UpdateTransactionStatusAsync(int transactionId, int status);
    Task UpdateTransactionAmountAsync(int transactionId, float amount);
    Task DeleteTransactionAsync(int transactionId);
}
