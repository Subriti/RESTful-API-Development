using System.ComponentModel.DataAnnotations;

namespace RESTful_API__ASP.NET_Core.Models
{
    public class CreationDto
    {
        //customized error msg
        [Required(ErrorMessage ="You should provide a name value.")]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(200)]
        public string? Description { get; set; }
    }
}
