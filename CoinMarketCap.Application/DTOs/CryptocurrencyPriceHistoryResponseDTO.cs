using CoinMarketCap.Domain.Models;

namespace CoinMarketCap.Application.DTOs;

public class CryptocurrencyPriceHistoryResponseDTO
{
    public List<CryptocurrencyPriceData> Data { get; set; }
}