using Microsoft.Extensions.Hosting;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using CoinMarketCap.Application.Extensions;

namespace CoinMarketCap.Application;

public partial class App
{
    private readonly IHost _host = ServicesConfigurator.CreateHost();

    protected override async void OnStartup(StartupEventArgs e)
    {
        await _host.StartAsync().ConfigureAwait(false);

        var serviceProvider = _host.Services.CreateScope().ServiceProvider;

        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await _host.StopAsync().ConfigureAwait(false);
        _host.Dispose();
        base.OnExit(e);
    }
}
