using Converter.ImageBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Converter.ImageConcrete
{
    public class ImagePpm : Image
    {
        public override Header Header { get; set; }
        public override string Path { get; set; }
        public override Color[,] Bitmap { get; set; }
    }
}
