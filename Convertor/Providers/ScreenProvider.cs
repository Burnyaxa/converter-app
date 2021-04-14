using System;
using System.Collections.Generic;
using System.Text;
using Converter.Interfaces;

namespace Converter.Providers
{
    public class ScreenProvider : IScreenProvider
    {
        public int GetWidth() => 200;


        public int GetHeight() => 200;

        public int GetFov() => 160;
    }
}
