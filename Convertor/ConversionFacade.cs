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
        public static void IniciateConversion(string originalPath, string destinationPath)
        {
            string originalTypeStr = GetExtension(originalPath);
            string finalTypeStr = GetExtension(destinationPath);
            ImageType originalType;
            ImageType finalType;

            try
            {
                originalType = (ImageType)Enum.Parse(typeof(ImageType), originalTypeStr, true);
                finalType = (ImageType)Enum.Parse(typeof(ImageType), finalTypeStr, true);
            }
            catch (Exception)
            {
                Console.WriteLine("Unsupported Image Type");
                return;
            }            
            
            IFactory<IImageReader> readerFactory = new ReaderFactory();
            IFactory<IImageWriter> writerFactory = new WriterFactory();
            IImageReader reader;
            IImageWriter writer;

            try
            {
                reader = readerFactory.Create(originalType);
                writer = writerFactory.Create(finalType);
                Image image = reader.Read(originalPath);
                Converter converter = new Converter(writer);
                converter.Convert(image, destinationPath);
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("You Fool! File Allready Exists! Stonp\n" + e.Message);
            }
             
            string GetExtension(string path)
            {
                return path.Substring(path.LastIndexOf('.')+1);
            }
        }

    }
}
