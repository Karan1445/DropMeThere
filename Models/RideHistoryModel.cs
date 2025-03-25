namespace dropmethereapi.Models
{
    public class RideHistoryModel
    {
        public int HistoryId { get; set; }
        public int SeekerUserID { get; set; }
        public int HelperUserID { get; set; }
        public int RequestID { get; set; }
        public DateTime RideFinishedAt { get; set; }
        public string RideRegardsFromSeeker { get; set; }
        public string HelpFromSeeker { get; set; }
        public string IsEnable { get; set; }
        public DateTime RideStartedAt { get; set; }
        public string SeekerUserName { get; set; }
        public string SeekerPhoneNumber { get; set; }
        public string SeekerCity { get; set; }
        public string SeekerState { get; set; }
        public string SeekerArea { get; set; }
        public string CurrentLocationLatLong { get; set; }
        public string RideStartPointLatLong { get; set; }
        public string RideEndPointLatLong { get; set; }
        public DateTime ReqTime { get; set; }
        public string HelperUserName { get; set; }
        public string StartPointName { get; set; }
        public string EndPointName { get; set; }
        public string Distance { get; set; }
        public string ReachableTime { get; set; }
    }
}
