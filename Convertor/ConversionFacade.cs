using Converter.ImageBase;
using Converter.Interfaces;
using Converter.Readers;
using Converter.Writers;
using System;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text;
using Color = System.Drawing.Color;

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
                
            var image = reader.Read(originalPath);
            Converter converter = new Converter(writer);
            converter.Convert(image, destinationPath);
            
            Bitmap pic = new Bitmap(image.Header.Width, image.Header.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            for (int i = 0; i < image.Header.Height; i++)
            {
                for (int j = 0; j < image.Header.Width; j++)
                {
                    Color c = Color.FromArgb(image.Bitmap[i, j].R, image.Bitmap[i, j].G, image.Bitmap[i, j].B);
                    pic.SetPixel(j, i, c);
                }
            }
            
            pic.Save("testcow.bmp");
            
            string GetExtension(string path)
            {
                return path.Substring(path.LastIndexOf('.') + 1);
            }
        }

    }
}
