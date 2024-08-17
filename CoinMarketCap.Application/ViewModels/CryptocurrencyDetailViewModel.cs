using CoinMarketCap.Application.Common;
using CoinMarketCap.Application.Models;
using CoinMarketCap.Application.Services.Singleton.Interfaces;
using CommunityToolkit.Mvvm.Input;

namespace CoinMarketCap.Application.ViewModels;

public partial class CryptocurrencyDetailViewModel(Cryptocurrency cryptocurrency, INavigationService navigationService)
    : PageViewModelBase
{
    public Cryptocurrency Cryptocurrency => cryptocurrency;

    [RelayCommand]
    private void GoBack()
    {
        navigationService.NavigateToDashboardPage();
    }
}