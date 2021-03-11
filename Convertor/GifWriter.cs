using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Converter.ImageBase;
using Converter.Interfaces;

namespace Converter
{
    public class GifWriter : IImageWriter
    {
        public void Write(string path, Image image)
        {
            Color[,] colors = GetColors(image.Bitmap);
            Color[] table = GetTable(colors);
            int tableSize = AlignToPowerOfTwo(table.Length);
            byte power = (byte) ((byte) Math.Log2(tableSize) - 1);
            string stringPower = Convert.ToString(power, 2);

            using BinaryWriter writer = new BinaryWriter(File.Open(path + ".gif", FileMode.Create));

            writer.Write("GIF87a");

            writer.Write(image.Header.Width);
            writer.Write(image.Header.Height);

            byte packedField = Convert.ToByte("11110" + stringPower, 2);
            writer.Write(packedField);

            byte bgColorIndex = 0;
            writer.Write(bgColorIndex);

            byte pixelAspectRatio = 0;
            writer.Write(pixelAspectRatio);

            for (int i = 0; i < tableSize; i++)
            {
                if (i < table.Length)
                {
                    writer.Write((byte)table[i].R);
                    writer.Write((byte)table[i].G);
                    writer.Write((byte)table[i].B);
                }
                else
                {
                    for (int j = 0; j < 3; j++)
                    {
                        writer.Write((byte)0);
                    }
                }
            }

            byte imageSeparator = 44;
            writer.Write(imageSeparator);
            short imageLeft = 0;
            writer.Write(imageLeft);
            short imageRight = 0;
            writer.Write(imageRight);
            writer.Write(image.Header.Width);
            writer.Write(image.Header.Height);
            byte imagePackedField = Convert.ToByte("00000000", 2);
            writer.Write(imagePackedField);





        }

        private static Color[,] GetColors(Color[,] colors)
        {
            int rows = colors.GetLength(0);
            int columns = colors.GetLength(1);

            Color[,] result = new Color[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    result[i, j].R = (colors[i, j].R * 8) / 256;
                    result[i, j].G = (colors[i, j].G * 8) / 256;
                    result[i, j].B = (colors[i, j].B * 4) / 256;
                }
            }

            return result;
        }

        private static Color[] GetTable(Color[,] colors)
        {
            Color[] result = colors.Cast<Color>().ToArray();
            return result.GroupBy(x => new {x.R, x.G, x.B})
                .Select(x => new Color()
                {
                    R = x.Key.R,
                    G = x.Key.G,
                    B = x.Key.B
                }).ToArray();
        }

        private static int AlignToPowerOfTwo(int length)
        {
            int minValue = 2;

            while (minValue <= length)
            {
                minValue *= 2;
            }

            return minValue;
        }
    }
}
