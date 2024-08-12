namespace CoinMarketCap.Application.Common;

public class PageChangedEventArgs : EventArgs
{
    public PageViewModelBase? CurrentPage { get; set; }
}