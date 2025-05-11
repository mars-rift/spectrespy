
using System.Collections.Generic;

namespace spectrespy.Models
{
    public class NetworkInfo
    {
        public string networkName { get; set; }
        public string blockCount { get; set; }
        public string headerCount { get; set; }
        public List<string> tipHashes { get; set; }
        public double difficulty { get; set; }
        public string pastMedianTime { get; set; }
        public List<string> virtualParentHashes { get; set; }
        public string pruningPointHash { get; set; }
        public string virtualDaaScore { get; set; }
    }

    public class PriceInfo
    {
        public double price { get; set; }
    }

    public class SpectredServer
    {
        public string spectredHost { get; set; }
        public string serverVersion { get; set; }
        public bool isUtxoIndexed { get; set; }
        public bool isSynced { get; set; }
        public string p2pId { get; set; }
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
        public List<SpectredServer> spectredServers { get; set; }
        public DatabaseInfo database { get; set; }
    }

    public class MarketCapInfo
    {
        public double marketcap { get; set; }
    }
}