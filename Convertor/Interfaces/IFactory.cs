using Converter.Readers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Converter.Interfaces
{
    public interface IFactory<CreationType>
    {
        CreationType Create(ImageType imageType);
    }
}
