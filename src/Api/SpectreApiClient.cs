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

        public async Task<HashrateInfo> GetHashrateInfoAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<HashrateInfo>("info/hashrate")
                    ?? new HashrateInfo();
            }
            catch (HttpRequestException)
            {
                return new HashrateInfo();
            }
        }

        public async Task<MaxHashrateInfo> GetMaxHashrateInfoAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<MaxHashrateInfo>("info/hashrate/max")
                    ?? new MaxHashrateInfo();
            }
            catch (HttpRequestException)
            {
                return new MaxHashrateInfo();
            }
        }

        public async Task<BlockRewardInfo> GetBlockRewardInfoAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<BlockRewardInfo>("info/blockreward")
                    ?? new BlockRewardInfo();
            }
            catch (HttpRequestException)
            {
                return new BlockRewardInfo();
            }
        }

        public async Task<HalvingInfo> GetHalvingInfoAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<HalvingInfo>("info/halving")
                    ?? new HalvingInfo();
            }
            catch (HttpRequestException)
            {
                return new HalvingInfo();
            }
        }

        public async Task<AddressLookup> GetAddressTransactionsAsync(string address)
        {
            try
            {
                var result = await _httpClient.GetFromJsonAsync<AddressLookup>($"addresses/{address}/transactions")
                    ?? new AddressLookup();
                result.address = address;
                return result;
            }
            catch (HttpRequestException)
            {
                return new AddressLookup { address = address };
            }
        }

        // Synchronous wrappers for the async methods
        public Task<NetworkInfo> GetNetworkInfo() => GetNetworkInfoAsync();
        public Task<PriceInfo> GetPriceInfo() => GetPriceInfoAsync();
        public Task<HealthInfo> GetHealthInfo() => GetHealthInfoAsync();
        public Task<MarketCapInfo> GetMarketCapInfo() => GetMarketCapInfoAsync();
        public Task<SpectredInfoModel> GetSpectredInfo() => GetSpectredInfoAsync();
        public Task<HashrateInfo> GetHashrateInfo() => GetHashrateInfoAsync();
        public Task<MaxHashrateInfo> GetMaxHashrateInfo() => GetMaxHashrateInfoAsync();
        public Task<BlockRewardInfo> GetBlockRewardInfo() => GetBlockRewardInfoAsync();
        public Task<HalvingInfo> GetHalvingInfo() => GetHalvingInfoAsync();
        public Task<AddressLookup> GetAddressTransactions(string address) => GetAddressTransactionsAsync(address);
    }
}
