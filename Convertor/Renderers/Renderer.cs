using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Converter.Interfaces;

namespace Converter.Renderers
{
    public class Renderer
    {
        private readonly ICameraPositionProvider _provider;

        public Renderer(ICameraPositionProvider provider)
        {
            _provider = provider;
        }


    }
}
