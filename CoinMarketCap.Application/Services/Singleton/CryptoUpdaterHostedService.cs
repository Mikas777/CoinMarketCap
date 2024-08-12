using CoinMarketCap.Application.Services.Transient.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace CoinMarketCap.Application.Services.Singleton;

public class CryptoUpdaterHostedService(IServiceProvider serviceProvider)
    : BackgroundService
{
    private readonly TimeSpan _interval = TimeSpan.FromSeconds(1);
    private readonly ManualResetEventSlim _startEvent = new(false);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Run(() => _startEvent.Wait(stoppingToken), stoppingToken).ConfigureAwait(false);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await Task.Delay(_interval, stoppingToken).ConfigureAwait(false);

                var currencyFetcherService = serviceProvider.GetRequiredService<ICurrencyFetcherService>();

                await currencyFetcherService.UpdateCryptocurrencies().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while updating cryptocurrency data.");
            }
        }
    }

    public void Start()
    {
        _startEvent.Set();
    }
}
