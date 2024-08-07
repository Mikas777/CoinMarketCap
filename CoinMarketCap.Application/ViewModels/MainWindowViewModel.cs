using CommunityToolkit.Mvvm.ComponentModel;

namespace CoinMarketCap.Application.ViewModels;

[ObservableObject]
public partial class MainWindowViewModel
{
    [ObservableProperty]
    private ObservableObject _content;

    public MainWindowViewModel(DashboardPageViewModel dashboardPageViewModel)
    {
        Content = dashboardPageViewModel;
    }
}