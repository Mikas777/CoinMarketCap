using System.IO;
using CoinMarketCap.Application.Clients;
using CoinMarketCap.Application.Clients.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CoinMarketCap.Application.Services.Singleton;
using CoinMarketCap.Application.Services.Singleton.Interfaces;
using CoinMarketCap.Application.Services.Transient;
using CoinMarketCap.Application.Services.Transient.Interfaces;
using Serilog;
using Serilog.Events;
using CoinMarketCap.Application.ViewModels;
using CoinMarketCap.Application.Models;
using CoinMarketCap.Application.Themes;
using CoinMarketCap.Application.Common.Enums;

namespace CoinMarketCap.Application.Extensions;

public class ServicesConfigurator
{
    public static IHost CreateHost()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("System.Net.Http.HttpClient", LogEventLevel.Warning)
            .WriteTo.Debug()
            .WriteTo.File($@"{Directory.GetCurrentDirectory()}\Logs\logs_.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 14).CreateLogger();

        return new HostBuilder()
            .ConfigureServices(services =>
            {
                services.AddHttpClient<ICoinCapClient, CoinCapClient>(client =>
                {
                    client.BaseAddress = new Uri("https://api.coincap.io/v2/");
                });

                services.AddTransient<IStartupService, StartupService>();
                services.AddTransient<ICurrencyFetcherService, CurrencyFetcherService>();
                services.AddSingleton<INavigationService, NavigationService>();
                services.AddSingleton<MainWindowViewModel>();
                services.AddSingleton<DashboardPageViewModel>();
                services.AddSingleton<RuntimeDataStorage>();
                services.AddSingleton<ThemeManager<ApplicationTheme>>();

                services.AddSingleton<CryptoUpdaterHostedService>();
                services.AddHostedService(static serviceProvider => serviceProvider.GetRequiredService<CryptoUpdaterHostedService>());

            }).UseSerilog().Build();
    }
}