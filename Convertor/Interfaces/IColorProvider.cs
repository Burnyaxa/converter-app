using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Converter.Interfaces
{
    public interface IColorProvider
    {
        public Vector3 GetBackgroundColor();
        public Vector3 GetObjectColor();
        public float GetBias();
    }
}
