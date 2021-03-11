using System;
using System.Collections.Generic;
using System.Text;

namespace Converter.BytePackers
{
    public static class LZWBytePacker
    {

        public static List<byte> PackBytes(List<int> codeStream, byte minCodeSize)
        {
            List<bool> codeBits = new List<bool>();
            List<byte> result = new List<byte>();

            int currentCodeWidth = minCodeSize + 1;
            int codeWidthIncrease = (int) Math.Pow(2, currentCodeWidth) - 1;

            for (int i = 0; i < codeStream.Count; i++)
            {
                int code = codeStream[i];

                if (i >= codeWidthIncrease)
                {
                    currentCodeWidth++;
                    codeWidthIncrease = (byte)((byte)Math.Pow(2, currentCodeWidth) - 1);
                }

                List<bool> bits = ConvertInt32ToBits(code, currentCodeWidth);
                codeBits.AddRange(bits);
            }

            int zeroesNeeded = 8 - (codeBits.Count % 8);

            for (int i = 0; i < zeroesNeeded; i++)
            {
                codeBits.Add(false);
            }

            for (int i = 0; i < codeBits.Count; i += 8)
            {
                List<bool> reversedByte = codeBits.GetRange(i, 8);
                reversedByte.Reverse();
                result.Add(ConvertBitsToByte(reversedByte));
            }

            return result;
        }

        private static List<bool> ConvertInt32ToBits(int num, int currentCodeWidth)
        {
            List<bool> bits = new List<bool>();

            int temp = num;

            while (temp > 0)
            {
                bits.Add((temp & 1) == 1);

                temp >>= 1;
            }

            while (bits.Count < currentCodeWidth)
            {
                bits.Add(false);
            }

            return bits;
        }

        private static byte ConvertBitsToByte(List<bool> bits)
        {
            byte result = 0;
            int length = bits.Count;
            int index = 8 - length;

            for (int i = 0; i < length; i++, index++)
            {
                if (bits[i])
                {
                    result |= (byte) (1 << (7 - index));
                }
            }

            return result;
        }
    }
}
