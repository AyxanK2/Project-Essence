using Essence.Repository1.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Essence.Repository1.Implementations
{
    public class EmailService : IEmailService
    {
        public async Task SendEmail(string email, string subject, string message)
        {
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential("ayxan.kerimli.2018@gmail.com", "ofwzetilqvfgzslu")
            };

            await client.SendMailAsync(new MailMessage(from: "ayxan.kerimli.2018@gmail.com", to: email, subject, message));
        }
    }
}
