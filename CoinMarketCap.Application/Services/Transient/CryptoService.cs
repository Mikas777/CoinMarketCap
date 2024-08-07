using CoinMarketCap.Application.Clients.Interfaces;
using CoinMarketCap.Application.Models;
using CoinMarketCap.Application.Services.Transient.Interfaces;

namespace CoinMarketCap.Application.Services.Transient;

public class CryptoService(ICoinCapClient coinCapClient) : ICryptoService
{
    public async Task<IEnumerable<Cryptocurrency>> GetCryptocurrencies()
    {
        var cryptocurrencyDtos = await coinCapClient.GetAssets().ConfigureAwait(false);

        if(cryptocurrencyDtos is null)
        {
            return [];
        }

        var cryptocurrencies = cryptocurrencyDtos.Select(x => new Cryptocurrency(x)).ToList();

        return cryptocurrencies;
    }
}