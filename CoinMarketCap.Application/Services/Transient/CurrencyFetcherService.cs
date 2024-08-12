using System.Diagnostics;
using CoinMarketCap.Application.Clients.Interfaces;
using CoinMarketCap.Application.Models;
using CoinMarketCap.Application.Services.Transient.Interfaces;

namespace CoinMarketCap.Application.Services.Transient;

public class CurrencyFetcherService(ICoinCapClient client, RuntimeDataStorage runtimeDataStorage) : ICurrencyFetcherService
{
    public async Task UpdateCryptocurrencies()
    {
        var incomingCryptocurrencies = await client.GetCryptocurrencies().ConfigureAwait(false);

        if (incomingCryptocurrencies is null)
        {
            return;
        }

        var currentCurrencies = runtimeDataStorage.Cryptocurrencies;
        var dict = currentCurrencies.ToDictionary(static i => i.Id);


        System.Windows.Application.Current.Dispatcher.Invoke(() =>
        {
            for (var i = 0; i < incomingCryptocurrencies.Count; i++)
            {
                var incomingItem = incomingCryptocurrencies[i];

                // Update existing
                if (dict.TryGetValue(incomingItem.Id, out var currentItem))
                {
                    if (currentItem.Id == incomingItem.Id)
                    {
                        currentItem.Rank = incomingItem.Rank;
                        currentItem.Supply = incomingItem.Supply;
                        currentItem.MaxSupply = incomingItem.MaxSupply;
                        currentItem.MarketCapUsd = incomingItem.MarketCapUsd;
                        currentItem.VolumeUsd24Hr = incomingItem.VolumeUsd24Hr;
                        currentItem.PriceUsd = incomingItem.PriceUsd;
                        currentItem.ChangePercent24Hr = incomingItem.ChangePercent24Hr;
                        currentItem.Vwap24Hr = incomingItem.Vwap24Hr;
                    }

                    if (currentCurrencies.IndexOf(currentItem) != i)
                    {
                        currentCurrencies.Move(currentCurrencies.IndexOf(currentItem), i);
                    }
                }
                // Add new
                else
                {
                    var newItem = new Cryptocurrency(incomingItem);
                    currentCurrencies.Insert(i, newItem);
                }
            }

            // Remove deleted
            for (var i = currentCurrencies.Count - 1; i >= 0; i--)
            {
                var itemToDelete = currentCurrencies.ElementAtOrDefault(i);

                if (itemToDelete is null)
                {
                    continue;
                }

                if (incomingCryptocurrencies.Any(x => x.Id == itemToDelete.Id))
                {
                    continue;
                }

                currentCurrencies.RemoveAt(i);
            }
        });

        Debug.WriteLine("Updated");
    }
}