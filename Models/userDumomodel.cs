using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dropmethereapi.Models
{
    public class userDumomodel
    {
        [Required]
        [EmailAddress]
        public String Email { get; set; }
        [Required]
        public String PassWord {  get; set; }
    }
    public class userRegsitermodel {
       
      

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
        public string ConfirmPassWord { get; set; }

        [Required]
        [MaxLength(15)]
        public string IsDriver { get; set; }

   

    }
}

