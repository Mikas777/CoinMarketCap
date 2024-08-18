using CoinMarketCap.Application.Services.Singleton;

namespace CoinMarketCap.Application.Common.Enums;

public enum ApplicationTheme
{
    [ResourceUriPath("Themes/LightTheme.xaml")]
    Light,

    [ResourceUriPath("Themes/DarkTheme.xaml")]
    Dark
}