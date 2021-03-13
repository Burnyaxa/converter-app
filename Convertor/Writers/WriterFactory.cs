using Converter.Interfaces;
using Converter.Readers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Converter.Writers
{
    public class WriterFactory : IFactory<IImageWriter>
    {
        public IImageWriter Create(ImageType imageType)
        {
            return imageType switch
            {
                ImageType.Gif => new GifWriter(),
                _ => null
            };
        }
    }
}
