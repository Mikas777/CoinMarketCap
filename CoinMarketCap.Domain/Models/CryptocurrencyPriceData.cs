namespace CoinMarketCap.Domain.Models;

public class CryptocurrencyPriceData
{
    public decimal PriceUsd { get; set; }

    public long Time { get; set; }

    public DateTime Date { get; set; }
}