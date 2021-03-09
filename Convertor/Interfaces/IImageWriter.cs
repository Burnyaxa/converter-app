using System;
using System.Collections.Generic;
using System.Text;

namespace Converter.Interfaces
{
    public interface IImageWriter
    {
        public void Write(string path, Image.Image image);
    }
}
