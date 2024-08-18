using CommunityToolkit.Mvvm.Input;
using ScottPlot;
using System.Windows.Media.Imaging;
using CoinMarketCap.Application.Common;
using CoinMarketCap.Application.Models;
using CoinMarketCap.Application.Services.Singleton.Interfaces;
using System.Diagnostics;

namespace CoinMarketCap.Application.ViewModels
{
    public partial class CryptocurrencyDetailViewModel(
        Cryptocurrency cryptocurrency,
        INavigationService navigationService,
        BitmapImage cryptocurrencyImage, Plot pointsPlot)
        : PageViewModelBase
    {
        public Cryptocurrency Cryptocurrency { get; } = cryptocurrency;
        public BitmapImage CryptocurrencyImage { get; } = cryptocurrencyImage;
        public Plot PointsPlot { get; } = pointsPlot;

        [RelayCommand]
        private void GoBack()
        {
            navigationService.NavigateToDashboardPage();
        }

        [RelayCommand]
        private void OpenUrl(string url)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }
    }
}