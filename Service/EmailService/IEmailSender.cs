using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Service
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message, IFormFile file = null);
        Task SendEmailWithMemoryStreamFileAsync(string email, string subject, string message, string fileName = null, string ContentType = null, byte[] file = null);
    }
}
