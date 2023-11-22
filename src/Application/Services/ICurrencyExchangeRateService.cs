namespace Application.Services;

public interface ICurrencyExchangeRateService
{
    Task<double> GetCurrentExchangeRateAsync(string currencyCode);
}
