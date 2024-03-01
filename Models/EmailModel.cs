using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Security;

namespace RESTful_API__ASP.NET_Core.Models
{
    public class EmailModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address.")] 
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.(com|net|org|gov|np|edu)$", ErrorMessage = "Invalid pattern.")]
        public string SenderEmail {  get; set; }
        [Required]
        public string SenderPassword {  get; set; }
        public string MailSubject {  get; set; }
        [Required]
        public string MailBody { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.(com|net|org|gov|np|edu)$", ErrorMessage = "Invalid pattern.")]
        public string ReceiverEmail {  get; set; }
    }
}
