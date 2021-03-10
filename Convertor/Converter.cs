using System;
using System.Collections.Generic;
using System.Text;
using Converter.ImageBase;
using Converter.Interfaces;

namespace Converter
{
    public class Converter : IConverter
    {
        private IReaderBase _reader;
        private IImageWriter _writer;

        public Converter(IReaderBase reader, IImageWriter writer)
        {
            _reader = reader;
            _writer = writer;
        }

        public Image Convert(Image image)
        {
            throw new NotImplementedException();
        }
    }
}
