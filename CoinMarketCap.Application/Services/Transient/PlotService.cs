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
        var pointsPlot = new Plot();
        
        pointsPlot.Add.Scatter(xs, priceData);
        pointsPlot.Axes.DateTimeTicksBottom();
        pointsPlot.Font.Set("Calibri");
        pointsPlot.Title($"Історія вартості {name} (1 рік.)");

        return pointsPlot;
    }

    private DateTime UnixTimeToDateTime(long unixTime)
    {
        return DateTimeOffset.FromUnixTimeMilliseconds(unixTime).DateTime;
    }
}