using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Converter.ImageBase;
using Converter.Interfaces;

namespace Converter
{
    public class GifWriter : IImageWriter
    {
        public void Write(string path, Image image)
        {
            using BinaryWriter writer = new BinaryWriter(File.Open(path + ".gif", FileMode.Create));

            writer.Write("GIF87a");

            writer.Write((short)image.Header.Width);
            writer.Write((short)image.Header.Height);

            byte packedField = Convert.ToByte("11110111", 2);
            writer.Write(packedField);

            byte bgColorIndex = 0;
            writer.Write(bgColorIndex);

            byte pixelAspectRatio = 0;
            writer.Write(pixelAspectRatio);


        }
    }
}
