using System;
using System.Collections.Generic;
using System.Text;

namespace Converter.Interfaces
{
    public interface IScreenProvider
    {
        public int GetWidth();
        public int GetHeight();
        public int GetFov();
    }
}
