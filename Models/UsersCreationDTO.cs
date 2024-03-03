using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RESTful_API__ASP.NET_Core.Models
{
    public class UsersCreationDTO
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public int CityId { get; set; }
        public UsersCreationDTO(string name, string email, string password, int cityId)
        {
            Name= name;
            Email= email;
            Password= password;
            CityId= cityId;
        }
    }
}
