namespace SunRaysMarket.Server.Application.Services;

public interface ICurrencyExchangeRateService
{
    Task<double> GetCurrentExchangeRateAsync(string currencyCode);
}
