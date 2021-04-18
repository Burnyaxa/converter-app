using System;
using System.Collections.Generic;
using System.Text;
using Converter.ImageBase;

namespace Converter.ImageConcrete
{
    public class ImageObj : Image
    {
        public override Header Header { get; set; }
        public override string Path { get; set; }
        public override Color[,] Bitmap { get; set; }
    }
}
