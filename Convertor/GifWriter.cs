using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Converter.BytePackers;
using Converter.Compressors;
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
            string stringPower =  Convert.ToString(power, 2).PadLeft(3, '0');

            List<int> codes = LzwCompressor.Compress(colors, table, tableSize);
            byte minCodeSize = (byte) (power + 1 < 2 ? 2 : power + 1);

            List<byte> bytes = LZWBytePacker.PackBytes(codes, minCodeSize);
            byte fullBlock = 255;

            int fullBlockQuantity = bytes.Count / fullBlock;
            byte partialBlockCount = (byte)(bytes.Count % fullBlock);

            using BinaryWriter writer = new BinaryWriter(File.Open(path + ".gif", FileMode.Create));

            writer.Write('G');
            writer.Write('I');
            writer.Write('F');
            writer.Write('8');
            writer.Write('9');
            writer.Write('a');
            writer.Write(image.Header.Width);
            writer.Write(image.Header.Height);

            byte packedField = Convert.ToByte("1" + stringPower + "0" + stringPower, 2);
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

            writer.Write(minCodeSize);

            for (int i = 0; i < fullBlockQuantity; i++)
            {
                writer.Write(fullBlock);
                for (int j = 0; j < fullBlock; j++)
                {
                    writer.Write(bytes[i * fullBlock + j]);
                }
            }

            if (partialBlockCount != 0)
            {
                writer.Write(partialBlockCount);
                for (int i = 0; i < partialBlockCount; i++)
                {
                    writer.Write(bytes[fullBlockQuantity * fullBlock + i]);
                }
            }

            byte blockTerminator = 0;

            writer.Write(blockTerminator);

            byte trailer = 59;

            writer.Write(trailer);
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
                    result[i, j] = new Color()
                    {
                        R = (colors[i, j].R * 8 / 256) * 36 ,
                        G = (colors[i, j].G * 8 / 256) * 36,
                        B = (colors[i, j].B * 4 / 256) * 72
                    };

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

            while (minValue < length)
            {
                minValue *= 2;
            }

            return minValue;
        }
    }
}
