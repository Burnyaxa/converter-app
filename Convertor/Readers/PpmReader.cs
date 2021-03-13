using Converter.ImageBase;
using Converter.ImageConcrete;
using Converter.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Converter.Readers
{
    public class PpmReader : ReaderBase
    {
        delegate int ReadSymbol();
        public override Color[,] ReadColors(Header header, BinaryReader imgFile)
        {
            return ((HeaderPpm) header).FormatType switch
            {
                "P6" => ReadP6(header, imgFile),
                _ => null
            };
        }

        public override Header ReadHeader(BinaryReader imgFile)
        {
            HeaderPpm header = new HeaderPpm();
            string type = String.Concat(imgFile.ReadChars(2));
            header.FormatType = type;

            char currentSymbol = imgFile.ReadChar();
            currentSymbol = imgFile.ReadChar();
            // processing comments
            while (currentSymbol == '#')
            {
                while(imgFile.ReadChar()!= '\n') { }
                currentSymbol = imgFile.ReadChar();
            }

            header.Width = ReadNextNumber();
            header.Height = ReadNextNumber();
            header.MaxNumPerColor = ReadNextNumber();
            
            if(header.MaxNumPerColor <= byte.MaxValue)
            {
                header.BitsPerComponent = 8;
            }
            else if(header.MaxNumPerColor <= Int16.MaxValue)
            {
                header.BitsPerComponent = 16;
            }
            else if (header.MaxNumPerColor <= Int32.MaxValue)
            {
                header.BitsPerComponent = 32;
            }

            return header;

            int ReadNextNumber()
            {
                string number = "";
                do
                {
                    number += currentSymbol;
                    currentSymbol = imgFile.ReadChar();
                }
                while (currentSymbol != '\n' && currentSymbol != ' ');
                return int.Parse(number);
            }
        }

        private Color[,] ReadP6(Header header, BinaryReader imgFile)
        {
            ReadSymbol readSymbol;

            if (header.BitsPerComponent > 255)
            {
                readSymbol = () => imgFile.ReadInt16();
            }
            else
            {
                readSymbol = () => imgFile.ReadByte();
            }

            Color[,] colors = new Color[header.Width, header.Height];

            try
            {    
                for (int i = 0; i < header.Width; i++)
                {
                    for (int j = 0; j < header.Height; j++)
                    {
                        colors[i, j] = new Color()
                        {
                            R = imgFile.ReadByte(),
                            G = imgFile.ReadByte(),
                            B = imgFile.ReadByte()
                        };
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Bad File XD");
            }
            finally
            {
                imgFile.Close();
            }
            NormalizeTo255(colors, (HeaderPpm)header);

            imgFile.Close();
            return colors;
        }

        private void NormalizeTo255(Color[,] colors, HeaderPpm header)
        {
            double coefficient = 255.0 / header.MaxNumPerColor;
            for (int i = 0; i < header.Width; i++)
            {
                for (int j = 0; j < header.Height; j++)
                {
                    colors[i, j].R = (int)(colors[i, j].R * coefficient);
                    colors[i, j].G = (int)(colors[i, j].G * coefficient);
                    colors[i, j].B = (int)(colors[i, j].B * coefficient);
                }
            }
        }
    }
}
