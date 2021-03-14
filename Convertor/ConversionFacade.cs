using Converter.ImageBase;
using Converter.Interfaces;
using Converter.Readers;
using Converter.Writers;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Converter
{
    public static class ConversionFacade
    {       
        public static void InitiateConversion(string originalPath, string destinationPath, string outputFormat)
        {
            string originalTypeStr = GetExtension(originalPath);
            ImageType originalType;
            ImageType finalType;

            try
            {
                originalType = (ImageType)Enum.Parse(typeof(ImageType), originalTypeStr, true);
                finalType = (ImageType)Enum.Parse(typeof(ImageType), outputFormat, true);
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Unsupported Image Type");
            }            
            
            IFactory<IImageReader> readerFactory = new ReaderFactory();
            IFactory<IImageWriter> writerFactory = new WriterFactory();
            IImageReader reader = readerFactory.Create(originalType);
            IImageWriter writer = writerFactory.Create(finalType);
                
            Image image = reader.Read(originalPath);
            Converter converter = new Converter(writer);
            converter.Convert(image, destinationPath);

             
            string GetExtension(string path)
            {
                return path.Substring(path.LastIndexOf('.') + 1);
            }
        }

    }
}
