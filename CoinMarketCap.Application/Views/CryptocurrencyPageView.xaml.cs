using CoinMarketCap.Application.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace CoinMarketCap.Application.Views;

/// <summary>
/// Логика взаимодействия для CryptocurrencyPageView.xaml
/// </summary>
public partial class CryptocurrencyPageView : UserControl
{
    public CryptocurrencyPageView()
    {
        InitializeComponent();
        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        var viewModel = (CryptocurrencyDetailViewModel)DataContext;
        var pointsPlot = viewModel.PointsPlot;

        WpfPlotPoints.Reset(pointsPlot);
        WpfPlotPoints.Refresh();
    }
}