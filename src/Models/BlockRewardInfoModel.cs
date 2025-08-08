namespace spectrespy.Models
{
    public class BlockRewardInfoModel
    {
        public double blockreward { get; set; }
        
        // Computed property for formatted display
        public string BlockRewardFormatted => $"{blockreward:N2} SPR";
    }
}
