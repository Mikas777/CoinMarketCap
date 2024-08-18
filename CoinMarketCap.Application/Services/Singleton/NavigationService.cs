using CoinMarketCap.Application.Common;
using CoinMarketCap.Application.Services.Singleton.Interfaces;
using CoinMarketCap.Application.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace CoinMarketCap.Application.Services.Singleton;

public class NavigationService(IServiceProvider serviceProvider) : INavigationService
{
    public PageViewModelBase? CurrentPage { get; private set; }

    public event EventHandler<PageChangedEventArgs>? PageChanged;

    public void NavigateTo(PageViewModelBase newPage)
    {
        if (CurrentPage == newPage)
        {
            return;
        }

        CurrentPage = newPage;
        PageChanged?.Invoke(this, new PageChangedEventArgs { CurrentPage = CurrentPage });
    }

    public void NavigateToDashboardPage()
    {
        var dashboardPageViewModel = serviceProvider.GetRequiredService<DashboardPageViewModel>();

        NavigateTo(dashboardPageViewModel);
    }
}