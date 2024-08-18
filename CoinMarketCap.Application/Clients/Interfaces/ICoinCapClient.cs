using CoinMarketCap.Application.DTOs;
using CoinMarketCap.Domain.Models;

namespace CoinMarketCap.Application.Clients.Interfaces;

public interface ICoinCapClient
{
    Task<List<CryptocurrencyDTO>?> GetCryptocurrencies();
    Task<List<CryptocurrencyPriceData>?> GetCryptocurrencyPriceHistory(string assetId, string interval = "m1");
}