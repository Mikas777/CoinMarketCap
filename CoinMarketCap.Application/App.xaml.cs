using Microsoft.Extensions.Hosting;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using CoinMarketCap.Application.ViewModels;
using CoinMarketCap.Application.Extensions;
using CoinMarketCap.Application.Services.Transient.Interfaces;

namespace CoinMarketCap.Application
{
    public partial class App
    {
        private readonly IHost _host = ServicesConfigurator.CreateHost();

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            await _host.StartAsync().ConfigureAwait(false);

            var serviceProvider = _host.Services.CreateScope().ServiceProvider;
            var mainWindowViewModel = serviceProvider.GetRequiredService<MainWindowViewModel>();

            var mainWindow = new MainWindow
            {
                DataContext = mainWindowViewModel
            };

            mainWindow.Show();

            await _host.Services.GetRequiredService<IStartupService>().Start().ConfigureAwait(false);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await _host.StopAsync().ConfigureAwait(false);
            _host.Dispose();
            base.OnExit(e);
        }
    }
}
