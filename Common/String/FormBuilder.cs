
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Common.String
{
    public static class FormBuilder
    {
        public static async Task<string> BuildForm(this string templatepath, string Id, string Link)
        {
            try
            {
                var builder = new BodyBuilder();
                using (StreamReader SourceReader = System.IO.File.OpenText(templatepath))
                {
                    builder.HtmlBody = await SourceReader.ReadToEndAsync();
                }
                string messageBody = builder.HtmlBody.Replace("{*Id*}", Id);
                messageBody = messageBody.Replace("{*Link*}", Link);
                return messageBody;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
