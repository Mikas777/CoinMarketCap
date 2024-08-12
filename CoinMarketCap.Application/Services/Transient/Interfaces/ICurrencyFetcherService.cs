namespace CoinMarketCap.Application.Services.Transient.Interfaces;

public interface ICurrencyFetcherService
{
    Task UpdateCryptocurrencies();
}