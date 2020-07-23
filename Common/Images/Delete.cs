using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Images
{
    public class Delete
    {
        public void DeleteImage(string path)
        {
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }
    }
}
