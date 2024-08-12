using CoinMarketCap.Application.DTOs;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CoinMarketCap.Application.Models;

[ObservableObject]
public partial class Cryptocurrency(CryptocurrencyDTO cryptocurrencyDTO)
{
    [ObservableProperty]
    private string _rank  = cryptocurrencyDTO.Rank;
    [ObservableProperty]
    private decimal _supply = cryptocurrencyDTO.Supply;
    [ObservableProperty]
    private decimal? _maxSupply = cryptocurrencyDTO.MaxSupply;
    [ObservableProperty]
    private decimal _marketCapUsd = cryptocurrencyDTO.MarketCapUsd;
    [ObservableProperty]
    private decimal _volumeUsd24Hr = cryptocurrencyDTO.VolumeUsd24Hr;
    [ObservableProperty]
    private decimal _priceUsd = cryptocurrencyDTO.PriceUsd;
    [ObservableProperty]
    private decimal _changePercent24Hr = cryptocurrencyDTO.ChangePercent24Hr;
    [ObservableProperty]
    private decimal _vwap24Hr = cryptocurrencyDTO.Vwap24Hr;
    [ObservableProperty]
    private string _priceChangeDirection;
    private decimal _previousPriceUsd;
  

    public string Id { get; } = cryptocurrencyDTO.Id;
    public string Symbol { get; } = cryptocurrencyDTO.Symbol;
    public string Name { get; } = cryptocurrencyDTO.Name;

    partial void OnPriceUsdChanged(decimal value)
    {
        PriceChangeDirection = value > _previousPriceUsd ? "Increased" : "Decreased";
        _previousPriceUsd = value;
    }
}
