namespace spectrespy.Models
{
    public class HalvingInfoModel
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
}
