using System.Windows;
using CoinMarketCap.Application.ViewModels;

namespace CoinMarketCap.Application
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = new MainWindowViewModel(new DashboardPageViewModel());
            InitializeComponent();
        }
    }
}