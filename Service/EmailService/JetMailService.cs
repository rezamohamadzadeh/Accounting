using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.EmailService
{
    public class JetMailService : IJetMailService
    {
        private readonly IConfiguration _Config;
        public JetMailService(IConfiguration config)
        {
            _Config = config;
        }

        public async Task<bool> SendEmailWithJetServiceAsync(string email, string subject, string message,string receiverName)
        {

            MailjetClient client = new MailjetClient(Environment.GetEnvironmentVariable(_Config["JetEmailConfig:MJ_APIKEY_PUBLIC"]), Environment.GetEnvironmentVariable(_Config["JetEmailConfig:MJ_APIKEY_PRIVATE"]))
            {
                Version = ApiVersion.V3_1,
            };
            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource,
            }
               .Property(Send.Messages, new JArray {
                new JObject {
                 {"From", new JObject {
                  {"Email", _Config["JetEmailConfig:Email"]},
                  {"Name", _Config["JetEmailConfig:Name"]}
                  }},
                 {"To", new JArray {
                  new JObject {
                   {"Email", email},
                   {"Name", receiverName}
                   }
                  }},
                 {"Subject", subject},
                 {"TextPart", _Config["JetEmailConfig:TextPart"]},
                 {"HTMLPart", message}
                 }
                   });
            MailjetResponse response = await client.PostAsync(request);
            if (response.IsSuccessStatusCode)
            {                return true;
            }
                //Console.WriteLine(string.Format("Total: {0}, Count: {1}\n", response.GetTotal(), response.GetCount()));
                //Console.WriteLine(response.GetData());

            else
            {
                return false;
                //Console.WriteLine(string.Format("StatusCode: {0}\n", response.StatusCode));
                //Console.WriteLine(string.Format("ErrorInfo: {0}\n", response.GetErrorInfo()));
                //Console.WriteLine(response.GetData());
                //Console.WriteLine(string.Format("ErrorMessage: {0}\n", response.GetErrorMessage()));
            }
        }
    }
}
