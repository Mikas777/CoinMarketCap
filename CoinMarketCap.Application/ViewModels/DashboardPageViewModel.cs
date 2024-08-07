using CoinMarketCap.Application.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using CoinMarketCap.Application.Models;

namespace CoinMarketCap.Application.ViewModels;

public partial class DashboardPageViewModel : ObservableObject
{
    private ObservableCollection<Cryptocurrency> _cryptocurrencies;

    private void UpdateCryptocurrencies()
    {

    }
}