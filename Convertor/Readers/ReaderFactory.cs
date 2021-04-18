using Converter.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Converter.Providers;
using Converter.Renderers;

namespace Converter.Readers
{
    public enum ImageType
    {
        Ppm,
        Obj,
        Gif
    }

    public class ReaderFactory : IFactory<IImageReader>
    {
        public IImageReader Create(ImageType imageType)
        {
            return imageType switch
            {
                ImageType.Ppm => new PpmReader(),
                ImageType.Obj => new ObjReader(new Renderer(new CameraPositionProvider(), new CameraDirectionProvider(),
                    new ScreenProvider(), new ColorProvider(), new LightsProvider()), new VectorConverter()),
                _ => null
            };
        }
    }
}
