using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Converter.ImageBase;

namespace Converter.Compressors
{
    public static class LzwCompressor
    {
        private static Dictionary<int, List<int>> dictionary;
        private const int MaxDictionarySize = 4096;

        public static List<int> Compress(Color[,] colors, Color[] table, int tableSize)
        {
            List<int> codeStream = new List<int>();

            List<int> indexStream = colors.Cast<Color>()
                .Select(x => Array.FindIndex(table, y => 
                    y.B == x.B && 
                    y.R == x.R &&
                    y.G == x.G)).ToList();

            Initialize(tableSize);
           
            codeStream.Add(tableSize);
            int index = 0;

            List<int> buffer = new List<int>() {indexStream[index]};

            for (int i = index + 1; i < indexStream.Count; i++)
            {
                List<int> code = new List<int>() { indexStream[i] };

                List<int> bufferWithCode = buffer.Concat(code).ToList();

                if (dictionary.Values.Any(x => x.SequenceEqual(bufferWithCode)))
                {
                    buffer = bufferWithCode;
                }
                else
                {
                    dictionary.Add(dictionary.Count, bufferWithCode);
                    codeStream.Add(GetCode(buffer));

                    if (dictionary.Count == MaxDictionarySize)
                    {
                        Initialize(tableSize);
                        codeStream.Add(tableSize);
                        buffer = new List<int>() {indexStream[i]};
                        continue;
                    }
                    buffer = code;
                }
            }
            
            codeStream.Add(GetCode(buffer));
            codeStream.Add(dictionary[tableSize + 1].First());

            return codeStream;
        }

        private static int GetCode(List<int> sequence)
        {
            return dictionary.Keys.FirstOrDefault(key => dictionary[key].SequenceEqual(sequence));
        }

        private static void Initialize(int tableSize)
        {
            dictionary = new Dictionary<int, List<int>>();
            for (int i = 0; i < tableSize; i++)
            {
                dictionary.Add(i, new List<int>() { i });
            }

            dictionary.Add(tableSize, new List<int>() { tableSize });
            dictionary.Add(tableSize + 1, new List<int>() { tableSize + 1 });
        }
    }
}
