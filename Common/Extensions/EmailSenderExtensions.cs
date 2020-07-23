using MimeKit;
using Service;
using System;
using System.IO;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Common.Extensions
{
    public static class EmailSenderExtensions
    {
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link)
        {
            return emailSender.SendEmailAsync(email, "Confirmation",
                $"Please Enter the Link: <a href='{HtmlEncoder.Default.Encode(link)}'>Link</a>");
        }
        public static Task SendDownladLinkAsync(this IEmailSender emailSender, string email, string link)
        {
            return emailSender.SendEmailAsync(email, "Download Link",
                $"Please Enter the Link: <a href='{HtmlEncoder.Default.Encode(link)}'>Link</a>");
        }
        public static Task SendDownladLinkAsync(this IEmailSender emailSender, string email, string link,string title,string text)
        {
            return emailSender.SendEmailAsync(email, $"{title}",
                $"{text} <br> Please Enter the Link: <a href='{HtmlEncoder.Default.Encode(link)}'>Link</a>");
        }

        public static Task SendEmailWithTemplate(this IEmailSender emailSender, string email, string title, string param1, string param2, string param3, string param4, string param5, string param6, string param7, string param8, string param9)
        {
            string template = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/EmailTemplate/template.html");
            string EmailData = BuildFactorForEmail(template, param1, param2, param3, param4, param5, param6, param7, param8, param9);
            return emailSender.SendEmailAsync(email, $"{title}", EmailData);
        }


        private static string BuildFactorForEmail(string templatepath, string fullName,
    string phone, string Email, string reservId, string reservdate,
    string statuse, string reserveDetail, string methodOfPayment, string amount)
        {
            try
            {
                var builder = new BodyBuilder();
                using (StreamReader SourceReader = System.IO.File.OpenText(templatepath))
                {
                    builder.HtmlBody = SourceReader.ReadToEnd();
                }
                
                string messageBody = string.Format(builder.HtmlBody, fullName,phone,Email,reservId,reservdate);
                
                return messageBody;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
