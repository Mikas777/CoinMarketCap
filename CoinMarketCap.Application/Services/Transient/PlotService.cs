using CoinMarketCap.Application.Clients.Interfaces;
using CoinMarketCap.Application.Services.Transient.Interfaces;
using ScottPlot;

namespace CoinMarketCap.Application.Services.Transient;

public class PlotService(ICoinCapClient coinCapClient) :IPlotService
{
    public async Task<Plot> GeneratePlotAsync(string name, string id)
    {
        var cryptocurrencyPriceHistory = await coinCapClient.GetCryptocurrencyPriceHistory(id).ConfigureAwait(false);

        if (cryptocurrencyPriceHistory == null)
        {
            return new Plot();
        }

        var priceData = cryptocurrencyPriceHistory.Select(p => Convert.ToDouble(p.PriceUsd)).ToArray();
        var dateTimes = cryptocurrencyPriceHistory.Select(p => UnixTimeToDateTime(p.Time)).ToArray();
        var xs = dateTimes.ToArray();
        var plotModel = new Plot();

        plotModel.Add.Scatter(xs, priceData);
        plotModel.Axes.DateTimeTicksBottom();
        plotModel.Title($"Історія вартості {name} (24 год.)");

        return plotModel;
    }

    private DateTime UnixTimeToDateTime(long unixTime)
    {
        return DateTimeOffset.FromUnixTimeMilliseconds(unixTime).DateTime;
    }
}