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

        public Vector3 GetObjectColor() => new Vector3(100, 100, 100);

        public float GetBias() => 0.2f;
    }
}
