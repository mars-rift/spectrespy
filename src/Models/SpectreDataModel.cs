
using System.Collections.Generic;
using System;

namespace spectrespy.Models
{
    public class NetworkInfo
    {
        public string? networkName { get; set; }
        public string? blockCount { get; set; }
        public string? headerCount { get; set; }
        public List<string>? tipHashes { get; set; }
        public double difficulty { get; set; }
        public string? pastMedianTime { get; set; }
        public List<string>? virtualParentHashes { get; set; }
        public string? pruningPointHash { get; set; }
        public string? virtualDaaScore { get; set; }
    }

    public class PriceInfo
    {
        public double price { get; set; }
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

    public class HealthInfo
    {
        public List<SpectredServer>? spectredServers { get; set; }
        public DatabaseInfo? database { get; set; }
    }

    public class MarketCapInfo
    {
        public double marketcap { get; set; }
    }

    public class HashrateInfo
    {
        public double hashrate { get; set; }
        
        // Computed properties for better display
        public string HashrateFormatted => FormatHashrate(hashrate);
        
        private string FormatHashrate(double hr)
        {
            if (hr >= 1e12) return $"{hr / 1e12:F2} TH/s";
            if (hr >= 1e9) return $"{hr / 1e9:F2} GH/s";
            if (hr >= 1e6) return $"{hr / 1e6:F2} MH/s";
            if (hr >= 1e3) return $"{hr / 1e3:F2} KH/s";
            if (hr >= 1) return $"{hr:F2} H/s";
            if (hr >= 1e-3) return $"{hr * 1e3:F2} mH/s";
            if (hr >= 1e-6) return $"{hr * 1e6:F2} μH/s";
            return $"{hr:E2} H/s";
        }
    }

    public class MaxHashrateInfo
    {
        public double hashrate { get; set; }
        public BlockHeader? blockheader { get; set; }
        
        // Computed properties for better display
        public string HashrateFormatted => FormatHashrate(hashrate);
        
        private string FormatHashrate(double hr)
        {
            if (hr >= 1e12) return $"{hr / 1e12:F2} TH/s";
            if (hr >= 1e9) return $"{hr / 1e9:F2} GH/s";
            if (hr >= 1e6) return $"{hr / 1e6:F2} MH/s";
            if (hr >= 1e3) return $"{hr / 1e3:F2} KH/s";
            if (hr >= 1) return $"{hr:F2} H/s";
            if (hr >= 1e-3) return $"{hr * 1e3:F2} mH/s";
            if (hr >= 1e-6) return $"{hr * 1e6:F2} μH/s";
            return $"{hr:E2} H/s";
        }
    }

    public class BlockHeader
    {
        public string? hash { get; set; }
        public string? timestamp { get; set; }
        public double difficulty { get; set; }
        public long daaScore { get; set; }
        public long blueScore { get; set; }
        
        public DateTime TimestampDateTime
        {
            get
            {
                if (DateTime.TryParse(timestamp, out var result))
                    return result;
                return DateTime.MinValue;
            }
        }
        
        public string FormattedTimestamp => TimestampDateTime.ToString("yyyy-MM-dd HH:mm:ss UTC");
        public string FormattedDifficulty => $"{difficulty:N0}";
    }

    public class BlockRewardInfo
    {
        public double blockreward { get; set; }
        
        // Computed property for formatted display
        public string BlockRewardFormatted => $"{blockreward:N2} SPR";
    }

    public class HalvingInfo
    {
        public long nextHalvingTimestamp { get; set; }
        public string? nextHalvingDate { get; set; }
        public double nextHalvingAmount { get; set; }
        
        // Computed properties for better display
        public DateTime NextHalvingDateTime => 
            DateTimeOffset.FromUnixTimeSeconds(nextHalvingTimestamp).DateTime;
        
        public string TimeUntilHalving
        {
            get
            {
                var timeSpan = NextHalvingDateTime - DateTime.UtcNow;
                if (timeSpan.TotalDays > 1)
                    return $"{timeSpan.Days} days, {timeSpan.Hours} hours";
                else if (timeSpan.TotalHours > 1)
                    return $"{timeSpan.Hours} hours, {timeSpan.Minutes} minutes";
                else if (timeSpan.TotalMinutes > 1)
                    return $"{timeSpan.Minutes} minutes";
                else
                    return "Soon";
            }
        }
        
        public string NextHalvingAmountFormatted => $"{nextHalvingAmount:N2} SPR";
    }

    public class AddressLookup
    {
        public string? address { get; set; }
        public List<Transaction>? transactions { get; set; }
        public int TransactionCount => transactions?.Count ?? 0;
    }

    public class Transaction
    {
        public string? transaction_id { get; set; }
        public string? block_hash { get; set; }
        public long block_time { get; set; }
        public bool is_accepted { get; set; }
        public List<TransactionInput>? inputs { get; set; }
        public List<TransactionOutput>? outputs { get; set; }
        
        public DateTime BlockDateTime => 
            DateTimeOffset.FromUnixTimeSeconds(block_time).DateTime;
        
        public string BlockTimeFormatted => BlockDateTime.ToString("yyyy-MM-dd HH:mm:ss UTC");
    }

    public class TransactionInput
    {
        public string? previous_outpoint_hash { get; set; }
        public int previous_outpoint_index { get; set; }
        public string? signature_script { get; set; }
        public long sequence { get; set; }
    }

    public class TransactionOutput
    {
        public long value { get; set; }
        public string? script_public_key { get; set; }
        public string? script_public_key_address { get; set; }
        public int script_public_key_type { get; set; }
        
        public string ValueFormatted => $"{value / 100000000.0:N8} SPR";
    }
}