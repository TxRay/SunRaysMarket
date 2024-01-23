namespace SunRaysMarket.Server.Application.Repositories;

public interface ITransactionRepository
{
    Task AddTransactionAsync(CreateTransactionModel model);
    Task<IEnumerable<TransactionDetailsModel>> GetTransactionsAsync(int customerId);

    Task<TransactionDetailsModel?> GetTransactionAsync(long transactionNumber);
    Task<TransactionDetailsModel?> GetTransactionAsync(int transactionId);
    Task<bool> UpdateTransactionMethodAsync(int transactionId, string method);
    Task<bool> UpdateTransactionStatusAsync(int transactionId, int status);
    Task<bool> UpdateTransactionAmountAsync(int transactionId, float amount);
    Task DeleteTransactionAsync(int transactionId);
}
