using Converter.Interfaces;
using Converter.Readers;
using Converter.Writers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Converter
{
    public static class ConversionFacade
    {       
        public static void IniciateConversion(string originalPath, string destinationPath, ImageType originalType, ImageType finalType)
        {
            IFactory<IImageReader> readerFactory = new ReaderFactory();
            IFactory<IImageWriter> writerFactory = new WriterFactory();
            IImageReader reader = readerFactory.Create(originalType);
            IImageWriter writer = writerFactory.Create(finalType);

        }

    }
}
