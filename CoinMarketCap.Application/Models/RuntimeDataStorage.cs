using System.Collections.ObjectModel;

namespace CoinMarketCap.Application.Models;

public class RuntimeDataStorage
{
    public ObservableCollection<Cryptocurrency> Cryptocurrencies { get; } = [];
}