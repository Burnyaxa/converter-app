using System;
using System.Collections.Generic;
using System.Text;
using Converter.ImageBase;

namespace Converter.Interfaces
{
    public interface IConverter
    {
        public void Convert(Image image, string destinationPath);
    }
}
