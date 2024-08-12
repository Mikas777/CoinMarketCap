using CoinMarketCap.Application.Common;
using CoinMarketCap.Application.Services.Singleton.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CoinMarketCap.Application.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty]
    private PageViewModelBase? _content;

    public MainWindowViewModel(INavigationService navigationService)
    {
        navigationService.PageChanged += OnPageChanged;
    }

    private void OnPageChanged(object? sender, PageChangedEventArgs e)
    {
        Content = e.CurrentPage;
    }
}