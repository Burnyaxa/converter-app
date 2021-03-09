using System;
using System.Collections.Generic;
using System.Text;
using Converter.Interfaces;

namespace Converter
{
    public class Converter : IConverter
    {
        private IImageReader _reader;
        private IImageWriter _writer;

        public Converter(IImageReader reader, IImageWriter writer)
        {
            _reader = reader;
            _writer = writer;
        }

        public Image.Image Convert(Image.Image image)
        {
            throw new NotImplementedException();
        }
    }
}
