using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageDetection
{
    public static class ImageHelper
    {

        public static MemoryStream LoadImageToMemoryStream(string fileName)
        {
            using (System.Drawing.Image image = System.Drawing.Image.FromFile(fileName))
            {
                MemoryStream m = new MemoryStream();
                image.Save(m, image.RawFormat);
                return m;
            }
        }
    }
}
