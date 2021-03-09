using System;
using System.Collections.Generic;
using System.Text;

namespace Convertor.Image
{
    public abstract class Header
    {
        public abstract int Height { get; set; }
        public abstract int Width { get; set; }
    }
}
