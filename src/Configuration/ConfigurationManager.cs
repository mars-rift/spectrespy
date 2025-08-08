using System;
using System.IO;
using System.Text.Json;

namespace spectrespy.Configuration
{
    public class AppConfig
    {
        public string ApiBaseUrl { get; set; } = "https://api.spectre-network.org/";
        public int DefaultRefreshInterval { get; set; } = 30;
        public bool AutoRefreshEnabled { get; set; } = true;
        public int RequestTimeoutSeconds { get; set; } = 10;
        public int MaxRetryAttempts { get; set; } = 3;
        public int RetryDelayMs { get; set; } = 1000;
        public bool EnableLogging { get; set; } = true;
        public string[] FavoriteAddresses { get; set; } = Array.Empty<string>();
        public bool SaveWindowPosition { get; set; } = true;
        public double WindowWidth { get; set; } = 1400;
        public double WindowHeight { get; set; } = 800;
        public double WindowLeft { get; set; } = double.NaN;
        public double WindowTop { get; set; } = double.NaN;
    }

    public class ConfigurationManager
    {
        private static readonly string ConfigFilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "SpectreSpy",
            "config.json");

        public static AppConfig LoadConfig()
        {
            try
            {
                if (File.Exists(ConfigFilePath))
                {
                    var json = File.ReadAllText(ConfigFilePath);
                    return JsonSerializer.Deserialize<AppConfig>(json) ?? new AppConfig();
                }
            }
            catch (Exception ex)
            {
                // Log error if logging is available
                System.Diagnostics.Debug.WriteLine($"Failed to load config: {ex.Message}");
            }

            return new AppConfig();
        }

        public static void SaveConfig(AppConfig config)
        {
            try
            {
                var directory = Path.GetDirectoryName(ConfigFilePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory!);
                }

                var json = JsonSerializer.Serialize(config, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                File.WriteAllText(ConfigFilePath, json);
            }
            catch (Exception ex)
            {
                // Log error if logging is available
                System.Diagnostics.Debug.WriteLine($"Failed to save config: {ex.Message}");
            }
        }
    }
}
