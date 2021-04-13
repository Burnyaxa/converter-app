using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Converter.Interfaces;

namespace Converter.Renderers
{
    public class Renderer : IRenderer
    {
        private readonly ICameraPositionProvider _positionProvider;
        private readonly ICameraDirectionProvider _directionProvider;

        public Renderer(ICameraPositionProvider positionProvider, ICameraDirectionProvider directionProvider)
        {
            _positionProvider = positionProvider;
            _directionProvider = directionProvider;
        }


        public Vector3[,] Render()
        {
            throw new NotImplementedException();
        }
    }
}
