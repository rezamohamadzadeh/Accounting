using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _Config;

        public EmailSender(IConfiguration configuration)
        {
            _Config = configuration;
        }
        public Task SendEmailAsync(string email, string subject, string message,IFormFile file = null)
        {
            MailMessage mail = new MailMessage();
            SmtpClient smtpServer = new SmtpClient(_Config["EmailConfig:smtp"]);
            mail.From = new MailAddress(_Config["EmailConfig:Email"], _Config["EmailConfig:Title"], Encoding.UTF8);
            mail.To.Add(email);
            mail.Subject = subject;
            mail.Body = message;
            mail.IsBodyHtml = true;
            smtpServer.Timeout = 20000;
            if (file != null)
            {
                string fileName = Path.GetFileName(file.FileName);
                mail.Attachments.Add(new Attachment(file  .OpenReadStream(), fileName));
            }


            smtpServer.Port = Convert.ToInt32(_Config["EmailConfig:Port"]);
            smtpServer.Credentials = new System.Net.NetworkCredential(_Config["EmailConfig:Email"], _Config["EmailConfig:Pass"]);
            smtpServer.EnableSsl = Convert.ToBoolean(_Config["EmailConfig:Ssl"]);   // this propertis is true  when your server support ssl

            smtpServer.Send(mail);
            return Task.CompletedTask;
        }
    }
}
