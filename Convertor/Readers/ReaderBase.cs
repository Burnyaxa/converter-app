using Converter.ImageBase;
using Converter.ImageConcrete;
using Converter.Interfaces;
using System.IO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Converter.Readers
{
    abstract class ReaderBase : IImageReader
    {
        public Image Read(string path)
        {
            FileStream imgFile = OpenBinary(path);
            Header header = ReadHeader(imgFile);
            var colors = ReadColors(header, imgFile);
            return new ImagePpm() { Path = path, Header = header, Bitmap=colors};
        }

        public virtual FileStream OpenBinary(string path)
        {
            return new FileStream(path, FileMode.Open);
        }

        public abstract Header ReadHeader(FileStream imgFile);
        public abstract Color[][] ReadColors(Header header, FileStream imgFile);
    }
}
