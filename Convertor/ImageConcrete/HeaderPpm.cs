using Converter.ImageBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Converter.ImageConcrete
{
    public class HeaderPpm : Header
    {
        public override int Height { get; set; }
        public override int Width { get; set; }
    }
}
