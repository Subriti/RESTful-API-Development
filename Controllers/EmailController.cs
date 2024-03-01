using Microsoft.AspNetCore.Mvc;
using RESTful_API__ASP.NET_Core.Models;
using System.Net;
using System.Net.Mail;

namespace RESTful_API__ASP.NET_Core.Controllers
{
    [ApiController]
    [Route("api/sendEmail")]
    public class EmailController : ControllerBase
    {
        [HttpPost]
        public ActionResult SendEmail(EmailModel email)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress(email.SenderEmail);
            message.Subject = email.MailSubject;
            message.To.Add(new MailAddress(email.ReceiverEmail));
            message.Body = $"<html><body>{email.MailBody}</body></html>";
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(email.SenderEmail, email.SenderPassword), //arge dada iycq hrgd
                EnableSsl = true
            };
            try
            {
                smtpClient.Send(message);
                return Ok(email);
            }
            catch(Exception ex)
            {
               return BadRequest(ex);
            }
        }
    }
}
