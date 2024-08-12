using System.Collections.ObjectModel;
using CoinMarketCap.Application.Models;
using CoinMarketCap.Application.Common;

namespace CoinMarketCap.Application.ViewModels;

public class DashboardPageViewModel(RuntimeDataStorage runtimeDataStorage) : PageViewModelBase
{
    public ObservableCollection<Cryptocurrency> Cryptocurrencies { get; } = runtimeDataStorage.Cryptocurrencies;
}