using CoinMarketCap.Application.Common;

namespace CoinMarketCap.Application.Services.Singleton.Interfaces;

public interface INavigationService
{
    void NavigateTo(PageViewModelBase newPage);
    void NavigateToDashboardPage();
    event EventHandler<PageChangedEventArgs>? PageChanged;
}