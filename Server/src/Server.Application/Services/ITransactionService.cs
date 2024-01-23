namespace SunRaysMarket.Server.Application.Services;

public interface ITransactionService
{
    Task CreateTransactionAsync(
        int orderId,
        float amountPaid,
        int billingAddressId,
        string chargeNumber
    );
}
