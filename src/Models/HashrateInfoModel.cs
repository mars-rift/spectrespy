namespace spectrespy.Models
{
    public class HashrateInfoModel
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
            return $"{hr:F2} H/s";
        }
    }
}
