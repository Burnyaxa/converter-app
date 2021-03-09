using System;
using System.Collections.Generic;
using System.Text;

namespace Convertor.Image
{
    public abstract class Image
    {
        public abstract Header Header { get; set; }
        public abstract string Path { get; set; }
    }
}
