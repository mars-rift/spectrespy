using System;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using spectrespy.Api;
using spectrespy.Models;
using System.Collections.Generic;

namespace spectrespy.ViewModels
{
    public class NetworkInfoModel
    {
        public string? networkName { get; set; }
        public string? blockCount { get; set; }
        public string? headerCount { get; set; }
        public double difficulty { get; set; }
        public string? pastMedianTime { get; set; }
        public string[]? tipHashes { get; set; }
        public string[]? virtualParentHashes { get; set; }
        public string? pruningPointHash { get; set; }
        public string? virtualDaaScore { get; set; }
    }

    public class PriceInfoModel
    {
        public double price { get; set; }
    }

    public class MarketCapInfoModel
    {
        public double marketcap { get; set; }
    }

    public class SpectredServer
    {
        public string? spectredHost { get; set; }
        public string? serverVersion { get; set; }
        public bool isUtxoIndexed { get; set; }
        public bool isSynced { get; set; }
        public string? p2pId { get; set; }
        public long blueScore { get; set; }
    }

    public class DatabaseInfo
    {
        public bool isSynced { get; set; }
        public long blueScore { get; set; }
        public int blueScoreDiff { get; set; }
    }

    public class HealthInfoModel
    {
        public SpectredServer[]? spectredServers { get; set; }
        public DatabaseInfo? database { get; set; }
    }

    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly HttpClient _client;
        public event PropertyChangedEventHandler? PropertyChanged;

        private NetworkInfoModel _networkInfo = new NetworkInfoModel();
        public NetworkInfoModel NetworkInfo
        {
            get => _networkInfo;
            set { _networkInfo = value; OnPropertyChanged(); }
        }

        private PriceInfoModel _priceInfo = new PriceInfoModel();
        public PriceInfoModel PriceInfo
        {
            get => _priceInfo;
            set { _priceInfo = value; OnPropertyChanged(); }
        }

        private MarketCapInfoModel _marketCapInfo = new MarketCapInfoModel();
        public MarketCapInfoModel MarketCapInfo
        {
            get => _marketCapInfo;
            set { _marketCapInfo = value; OnPropertyChanged(); }
        }

        private HealthInfoModel _healthInfo = new HealthInfoModel();
        public HealthInfoModel HealthInfo
        {
            get => _healthInfo;
            set { _healthInfo = value; OnPropertyChanged(); }
        }

        private string _lastUpdatedTime = string.Empty;
        public string LastUpdatedTime
        {
            get => _lastUpdatedTime;
            set { _lastUpdatedTime = value; OnPropertyChanged(); }
        }

        private string _statusMessage = string.Empty;
        public string StatusMessage
        {
            get => _statusMessage;
            set { _statusMessage = value; OnPropertyChanged(); }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set { _isLoading = value; OnPropertyChanged(); }
        }

        private AsyncRelayCommand? _refreshCommand;
        public ICommand RefreshCommand
        {
            get
            {
                _refreshCommand ??= new AsyncRelayCommand(LoadAllDataAsync, () => !IsLoading);
                return _refreshCommand;
            }
        }

        public MainViewModel()
        {
            _client = new HttpClient();
            StatusMessage = "Loading data...";
            // Start loading data
            _ = LoadAllDataAsync();
        }

        private async Task LoadAllDataAsync()
        {
            IsLoading = true;
            StatusMessage = "Refreshing data...";
            _refreshCommand?.RaiseCanExecuteChanged();

            var errors = new List<string>();

            var tasks = new List<Task>
            {
                Task.Run(async () => {
                    try { await LoadNetworkInfoAsync(); }
                    catch (Exception ex) { errors.Add("Network info: " + ex.Message); }
                }),
                Task.Run(async () => {
                    try { await LoadPriceInfoAsync(); }
                    catch (Exception ex) { errors.Add("Price info: " + ex.Message); }
                }),
                Task.Run(async () => {
                    try { await LoadMarketCapInfoAsync(); }
                    catch (Exception ex) { errors.Add("Market cap: " + ex.Message); }
                }),
                Task.Run(async () => {
                    try { await LoadHealthInfoAsync(); }
                    catch (Exception ex) { errors.Add("Health info: " + ex.Message); }
                })
            };

            await Task.WhenAll(tasks);

            LastUpdatedTime = DateTime.Now.ToString("g");
            StatusMessage = errors.Count == 0 ? "Data loaded successfully" : "Some data failed: " + string.Join("; ", errors);
            IsLoading = false;
            _refreshCommand?.RaiseCanExecuteChanged();
        }

        private async Task<T?> RetryAsync<T>(Func<Task<T>> action, int maxAttempts = 3, int delayMs = 1000)
        {
            for (int attempt = 1; attempt <= maxAttempts; attempt++)
            {
                try
                {
                    return await action();
                }
                catch when (attempt < maxAttempts)
                {
                    await Task.Delay(delayMs);
                }
            }
            return default;
        }

        private async Task LoadNetworkInfoAsync()
        {
            var result = await RetryAsync(async () =>
            {
                var json = await _client.GetStringAsync("https://api.spectre-network.org/info/network");
                return JsonSerializer.Deserialize<NetworkInfoModel>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            });

            if (result != null)
                NetworkInfo = result;
        }

        private async Task LoadPriceInfoAsync()
        {
            var result = await RetryAsync(async () =>
            {
                var json = await _client.GetStringAsync("https://api.spectre-network.org/info/price?stringOnly=false");
                return JsonSerializer.Deserialize<PriceInfoModel>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            });

            if (result != null)
                PriceInfo = result;
        }

        private async Task LoadMarketCapInfoAsync()
        {
            var result = await RetryAsync(async () =>
            {
                var json = await _client.GetStringAsync("https://api.spectre-network.org/info/marketcap?stringOnly=false");
                return JsonSerializer.Deserialize<MarketCapInfoModel>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            });

            if (result != null)
                MarketCapInfo = result;
        }

        private async Task LoadHealthInfoAsync()
        {
            var result = await RetryAsync(async () =>
            {
                var json = await _client.GetStringAsync("https://api.spectre-network.org/info/health");
                return JsonSerializer.Deserialize<HealthInfoModel>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            });

            if (result != null)
                HealthInfo = result;
        }

        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object>? _canExecute;

        public RelayCommand(Action<object> execute, Predicate<object>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object? parameter) => _canExecute == null || _canExecute(parameter!);
        public void Execute(object? parameter) => _execute(parameter!);
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}