using CoinMarketCap.Application.DTOs;

namespace CoinMarketCap.Application.Clients.Interfaces;

public interface ICoinCapClient
{
    Task<List<CryptocurrencyDTO>?> GetCryptocurrencies();
}