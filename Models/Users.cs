using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RESTful_API__ASP.NET_Core.Models
{
    public class Users
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        //[EmailAddress(ErrorMessage = "Invalid email address.")]
        //[RegularExpression(@"^[^@\s]+@[^@\s]+\.(com|net|org|gov|np|edu)$", ErrorMessage = "Invalid pattern.")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        [ForeignKey(nameof(CityId))]
        public int CityId { get; set; }
        [JsonIgnore] // not showing full city with point of interest detail
        public CityDto? City { get; set; }

        public Users(string name, string email, string password, int cityId)
        {
            Name= name;
            Email= email;
            Password= password;
            CityId= cityId;
        }
    }
}
