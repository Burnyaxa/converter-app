using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Converter.Interfaces;

namespace Converter.Renderers
{
    public class Renderer
    {
        private readonly ICameraPositionProvider _positionProvider;
        private readonly ICameraDirectionProvider _directionProvider;

        public Renderer(ICameraPositionProvider positionProvider, ICameraDirectionProvider directionProvider)
        {
            _positionProvider = positionProvider;
            _directionProvider = directionProvider;
        }


    }
}
