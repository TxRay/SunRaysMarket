namespace SunRaysMarket.Server.Core.Services;

public interface ICurrencyExchangeRateService
{
    Task<double> GetCurrentExchangeRateAsync(string currencyCode);
}
