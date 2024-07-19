using System.Text.Json.Serialization;

namespace DriveSalez.Domain.Entities
{
    public class AccountPhoneNumber
    {
        [JsonIgnore]
        public int Id { get; set; }

        public string PhoneNumber { get; set; }
    }
}
