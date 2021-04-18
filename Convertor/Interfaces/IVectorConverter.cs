using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Converter.ImageBase;

namespace Converter.Interfaces
{
    public interface IVectorConverter
    {
        public Color[,] ConvertFromVectorToColors(Vector3[,] vectors);
    }
}
