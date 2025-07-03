using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using spectrespy.Models;

namespace spectrespy.Api
{
    public class SpectreApiClient
    {
        private readonly HttpClient _httpClient;

        public SpectreApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            // Ensure the HttpClient has a base address set
            if (_httpClient.BaseAddress == null)
            {
                _httpClient.BaseAddress = new System.Uri("https://api.spectre-network.org/");
            }
        }

        public async Task<NetworkInfo> GetNetworkInfoAsync()
        {
            try
            {
                // This matches the URL shown in your curl example
                return await _httpClient.GetFromJsonAsync<NetworkInfo>("info/network")
                    ?? new NetworkInfo();
            }
            catch (HttpRequestException)
            {
                // Return default data on failure
                return new NetworkInfo();
            }
        }

        public async Task<PriceInfo> GetPriceInfoAsync()
        {
            try
            {
                // This matches the URL shown in your curl example
                return await _httpClient.GetFromJsonAsync<PriceInfo>("info/price?stringOnly=false")
                    ?? new PriceInfo();
            }
            catch (HttpRequestException)
            {
                // Return default data on failure
                return new PriceInfo();
            }
        }

        public async Task<HealthInfo> GetHealthInfoAsync()
        {
            try
            {
                // This matches the URL shown in your curl example
                return await _httpClient.GetFromJsonAsync<HealthInfo>("info/health")
                    ?? new HealthInfo();
            }
            catch (HttpRequestException)
            {
                // Return default data on failure
                return new HealthInfo();
            }
        }

        public async Task<MarketCapInfo> GetMarketCapInfoAsync()
        {
            try
            {
                // This matches the URL shown in your curl example
                return await _httpClient.GetFromJsonAsync<MarketCapInfo>("info/marketcap?stringOnly=false")
                    ?? new MarketCapInfo();
            }
            catch (HttpRequestException)
            {
                // Return default data on failure
                return new MarketCapInfo();
            }
        }

        public async Task<SpectredInfoModel> GetSpectredInfoAsync()
        {
            try
            {
                // This matches the new spectred endpoint
                return await _httpClient.GetFromJsonAsync<SpectredInfoModel>("info/spectred")
                    ?? new SpectredInfoModel();
            }
            catch (HttpRequestException)
            {
                // Return default data on failure
                return new SpectredInfoModel();
            }
        }

        // Synchronous wrappers for the async methods
        public Task<NetworkInfo> GetNetworkInfo() => GetNetworkInfoAsync();
        public Task<PriceInfo> GetPriceInfo() => GetPriceInfoAsync();
        public Task<HealthInfo> GetHealthInfo() => GetHealthInfoAsync();
        public Task<MarketCapInfo> GetMarketCapInfo() => GetMarketCapInfoAsync();
        public Task<SpectredInfoModel> GetSpectredInfo() => GetSpectredInfoAsync();
    }
}
