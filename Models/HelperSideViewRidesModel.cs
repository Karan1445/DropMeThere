using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace dropmethereapi.Models
{
    public class HelperSideViewRidesModel
    {
        [Key]
        public int? ConfirmationID { get; set; } // Nullable Primary Key

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
    }
}
