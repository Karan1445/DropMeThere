namespace dropmethereapi.Models
{
    public class SeekerRequestHandlerModel
    {
       
            public int RequestID { get; set; } // Primary Key, will be auto-generated
            public int UserID { get; set; } // Foreign Key
            public string UserName { get; set; }
            public string PhoneNumber { get; set; }
            public string SeekerCity { get; set; } 
            public string SeekerState { get; set; }
            public string SeekerArea { get; set; }
            public string CurrentLocationLatLong { get; set; } 
            public string RideStartPointLatLong { get; set; } 
            public string RideEndPointLatLong { get; set; }
            public DateTime ReqTime { get; set; }
        public String StartPointName { get; set; }
        public String EndPointName { get; set; }    

        public String Distance { get; set; }
        public String ReachableTime { get; set; }
    }
}
