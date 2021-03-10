using System;
using System.Collections.Generic;
using System.Text;
using Converter.ImageBase;

namespace Converter.Interfaces
{
    public interface IReaderBase
    {
        public Image Read(string path);
    }
}
