using System.Text.Json.Serialization;

namespace StudentManagementSystem.Models
{
    public class UserLogin
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}