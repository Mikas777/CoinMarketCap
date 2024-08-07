using CoinMarketCap.Application.Models;

namespace CoinMarketCap.Application.Services.Transient.Interfaces;

public interface ICryptoService
{
    Task<IEnumerable<Cryptocurrency>> GetCryptocurrencies();
}