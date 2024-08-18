using ScottPlot;

namespace CoinMarketCap.Application.Services.Transient.Interfaces;

public interface IPlotService
{
    Task<Plot> GeneratePlotAsync(string name, string id);
}