using Converter.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Converter.Readers
{
    public enum ImageType
    {
        Ppm,
        Gif
    }

    public class ReaderFactory : IFactory<IImageReader>
    {
        public IImageReader Create(ImageType imageType)
        {
            switch (imageType)
            {
                case ImageType.Ppm:
                    return new PpmReader();
                default:
                    return null;

            }
        }
    }
}
