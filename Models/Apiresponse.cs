using System.Text.Json.Serialization;

namespace dropmethereapi.Models
{
    public class Apiresponse
    {
        [JsonPropertyName("value")]
        public ValueData Value { get; set; }
    }

    public class ValueData
    {
        [JsonPropertyName("token")]
        public string? Token { get; set; }

        [JsonPropertyName("user")]
        public UserData? User { get; set; }
    }

    public class UserData
    {
        [JsonPropertyName("userID")]
        public int UserID { get; set; }

        [JsonPropertyName("userName")]
        public string UserName { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonPropertyName("passWord")]
        public string PassWord { get; set; }

        [JsonPropertyName("isDriver")]
        public string IsDriver { get; set; }

        [JsonPropertyName("isVehicalRegistered")]
        public string IsVehicalRegistered { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime? CreatedAt { get; set; }

        [JsonPropertyName("modifiedAt")]
        public DateTime? ModifiedAt { get; set; }
    }
}
