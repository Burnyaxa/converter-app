using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Converter.Interfaces;

namespace Converter.Providers
{
    public class CameraPositionProvider : ICameraPositionProvider
    {
        public Vector3 GetCamera()
        {
            return Vector3.Zero;
        }
    }
}
