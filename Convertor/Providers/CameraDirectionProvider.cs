using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Converter.Interfaces;

namespace Converter.Providers
{
    public class CameraDirectionProvider : ICameraDirectionProvider
    {
        public Vector3 GetCameraDirection()
        {
            return new Vector3(0, 0, 1);
        }
    }
}
