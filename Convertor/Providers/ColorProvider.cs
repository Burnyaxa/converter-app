using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Converter.Interfaces;

namespace Converter.Providers
{
    public class ColorProvider : IColorProvider
    {
        public Vector3 GetBackgroundColor() => new Vector3(255, 255, 255);
//24, 9, 232
        public Vector3 GetObjectColor() => new Vector3(108, 34, 25);

        public float GetBias() => 0.5f;
    }
}
