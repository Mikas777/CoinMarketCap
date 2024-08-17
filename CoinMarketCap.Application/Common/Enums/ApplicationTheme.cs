using CoinMarketCap.Application.Services.Singleton;
using CoinMarketCap.Application.Themes;

namespace CoinMarketCap.Application.Common.Enums;

public enum ApplicationTheme
{
    [ResourceUriPath("Themes/LightTheme.xaml")]
    Light,

    [ResourceUriPath("Themes/DarkTheme.xaml")]
    Dark
}