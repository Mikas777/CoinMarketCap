using CoinMarketCap.Application.Services.Singleton;
using CoinMarketCap.Application.Services.Singleton.Interfaces;
using CoinMarketCap.Application.Services.Transient.Interfaces;

namespace CoinMarketCap.Application.Services.Transient;

public class StartupService(ICurrencyFetcherService currencyFetcherService, INavigationService navigationService, CryptoUpdaterHostedService cryptoUpdaterHostedService) : IStartupService
{
    public async Task Start()
    {
        navigationService.NavigateToDashboardPage();

        await currencyFetcherService.UpdateCryptocurrencies().ConfigureAwait(false);
        cryptoUpdaterHostedService.Start();
    }
}