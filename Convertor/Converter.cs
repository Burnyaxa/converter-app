using System;
using System.Collections.Generic;
using System.Text;
using Converter.ImageBase;
using Converter.Interfaces;

namespace Converter
{
    public class Converter : IConverter
    {
        private readonly IImageWriter _writer;

        public Converter(IImageWriter writer)
        {
            _writer = writer;
        }

        public void Convert(Image image, string destinationPath)
        {
            _writer.Write(destinationPath, image);
        }
    }
}
