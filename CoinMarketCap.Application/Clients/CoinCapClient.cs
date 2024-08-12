using System.Net.Http;
using CoinMarketCap.Application.Clients.Interfaces;
using CoinMarketCap.Application.DTOs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CoinMarketCap.Application.Clients;

public class CoinCapClient(HttpClient httpClient, ILogger<CoinCapClient> logger) : ICoinCapClient
{
    public async Task<List<CryptocurrencyDTO>?> GetCryptocurrencies()
    {
        const string url = "assets";

        try
        {
            var response = await httpClient.GetAsync(url).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                logger.LogError("Request to {Url:l}\nfailed with status code: {StatusCode} - {Status:l}:\n{ResponseContent:l}", url, (int)response.StatusCode, response.StatusCode.ToString(), await response.Content.ReadAsStringAsync().ConfigureAwait(false));
                
                return null;
            }

            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            try
            {
                var data = JsonConvert.DeserializeObject<CryptocurrencyResponseDTO>(content);

                return data?.Data;
            }
            catch (Exception e)
            {
                logger.LogCritical(e, "Error deserialize data: {ExceptionMessage}", e.Message);

                return null;
            }
        }
        catch (Exception e)
        {
            logger.LogCritical(e, "Error calling {Url}: {ExceptionMessage}", $"{httpClient.BaseAddress}{url}", e.Message);

            return null;
        }
    }
}