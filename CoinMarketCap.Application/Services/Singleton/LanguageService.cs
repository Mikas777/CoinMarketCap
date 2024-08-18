using System.Globalization;
using System.Windows;

namespace CoinMarketCap.Application.Services.Singleton;

public class LanguageService
{
    public void ChangeLanguage(string cultureCode)
    {
        var culture = new CultureInfo(cultureCode);
        Thread.CurrentThread.CurrentUICulture = culture;

        var dashboardDictionary = new ResourceDictionary
        {
            Source = new Uri($"Resources/DashboardPage/DashboardPage.{cultureCode}.xaml", UriKind.Relative)
        };

        var cryptocurrencyDetail = new ResourceDictionary
        {
            Source = new Uri($"Resources/CryptocurrencyPage/CryptocurrencyPage.{cultureCode}.xaml", UriKind.Relative)
        };

        System.Windows.Application.Current.Resources.MergedDictionaries.Clear();
        System.Windows.Application.Current.Resources.MergedDictionaries.Add(dashboardDictionary);
        System.Windows.Application.Current.Resources.MergedDictionaries.Add(cryptocurrencyDetail);
    }
}