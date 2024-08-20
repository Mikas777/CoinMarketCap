using CoinMarketCap.Domain.Models;

namespace CoinMarketCap.Application.DTOs;

public class CryptocurrencyPriceHistoryResponseDTO
{
    public List<CryptocurrencyPriceDateHistoryDTO> Data { get; set; }
}