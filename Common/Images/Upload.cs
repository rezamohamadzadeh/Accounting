using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Common.Images
{
    public class Upload:IDisposable
    {
        private FileStream _stream;

        public async Task UploadImage(string path,string DirectoryPath, IFormFile file)
        {
            if (!Directory.Exists(DirectoryPath))
            {
                Directory.CreateDirectory(DirectoryPath);
            }
            _stream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(_stream);
            _stream.Dispose();
        }

        public void Dispose()
        {
            _stream.Dispose();
        }

    }
}
