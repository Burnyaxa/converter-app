using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Converter.Interfaces
{
    public interface ICameraPositionProvider
    {
        public Vector3 GetCamera();
    }
}
