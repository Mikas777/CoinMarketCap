using CoinMarketCap.Application.DTOs;
using Newtonsoft.Json.Linq;

namespace CoinMarketCap.Application.Clients.Interfaces;

public interface ICoinCapClient
{
    Task<List<CryptocurrencyDTO>?> GetAssets();
}