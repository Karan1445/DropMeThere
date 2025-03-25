using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace dropmethereapi.Models
{
    public class User
    {
        

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? UserID { get; set; }

        [Required]
        [MaxLength(150)]
        public string? UserName { get; set; }

        [MaxLength(200)]
        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(15)]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(500)]
        public string PassWord { get; set; }

        [Required]
        [MaxLength(15)]
        public string IsDriver { get; set; }

        [MaxLength(15)]
        public string IsVehicalRegistered { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? ModifiedAt { get; set; }
    }
}
