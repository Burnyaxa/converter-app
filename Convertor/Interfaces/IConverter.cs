using System;
using System.Collections.Generic;
using System.Text;

namespace Converter.Interfaces
{
    public interface IConverter
    {
        public Image.Image Convert(Image.Image image);
    }
}
