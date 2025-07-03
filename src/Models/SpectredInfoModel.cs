namespace spectrespy.Models
{
    public class SpectredInfoModel
    {
        public string? mempoolSize { get; set; }
        public string? serverVersion { get; set; }
        public bool isUtxoIndexed { get; set; }
        public bool isSynced { get; set; }
        public string? p2pIdHashed { get; set; }
    }
}
