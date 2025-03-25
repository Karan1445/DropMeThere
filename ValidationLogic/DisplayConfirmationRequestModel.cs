using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace dropmethereapi.ValidationLogic
{
    public class DisplayConfirmationRequestModel
    {
       
        public int? ConfrimationID { get; set; } // Nullable Primary Key

        [ForeignKey("SeekerUser")]
        public int SeekerUserID { get; set; }

        [ForeignKey("HelperUser")]
        public int HelperUserID { get; set; }

        [ForeignKey("SeekerRequest")]
        public int RequestID { get; set; }

        [MaxLength(1200)]
        public string HelpersCurrentLocationLatLong { get; set; }

        public DateTime ConfirmationTime { get; set; }

        [MaxLength(550)]
        public string HelperRechabletimetoStartPoint { get; set; }

        [MaxLength(550)]
        public string HelperDistanceFromStartPoint { get; set; }

        public string HelperUserName { get; set; }
        public string HelperPhoneNumber { get; set; }
        public string SeekerUserName { get; set; }
        public string SeekerPhoneNumber { get; set; }
        public string StartPointName { get; set; }
        public string EndPointName { get; set; }
        public string RidedataReachableTime { get; set; }
        public string RidedataDistance { get; set; }
    
}
}
