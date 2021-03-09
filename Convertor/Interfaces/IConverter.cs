using System;
using System.Collections.Generic;
using System.Text;
using Convertor.Image;
namespace Convertor.Interfaces
{
    public interface IConverter
    {
        public Image.Image Convert(Image.Image image);
    }
}
