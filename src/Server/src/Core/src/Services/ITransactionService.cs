namespace SunRaysMarket.Server.Core.Services;

public interface ITransactionService
{
    Task CreateTransactionAsync(int orderId, float amountPaid, string chargeNumber);
}