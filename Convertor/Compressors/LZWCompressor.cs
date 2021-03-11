using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Converter.ImageBase;

namespace Converter.Compressors
{
    public static class LzwCompressor
    {

        public static List<int> Compress(Color[,] colors, Color[] table, int tableSize)
        {
            List<int> codeStream = new List<int>();

            List<int> indexStream = colors.Cast<Color>()
                .Select(x => Array.FindIndex(table, y => 
                    y.B == x.B && 
                    y.R == x.R &&
                    y.G == x.G)).ToList();

            Dictionary<int, List<int>> dictionary = new Dictionary<int, List<int>>();

            for (int i = 0; i < tableSize; i++)
            {
                dictionary.Add(i, new List<int>(){i});
            }

            dictionary.Add(tableSize, new List<int>(){tableSize});
            dictionary.Add(tableSize + 1, new List<int>(){tableSize + 1});

            int index = 0;

            List<int> buffer = new List<int>() {indexStream[index + 1]};

            for (int i = index + 1; i < indexStream.Count; i++)
            {
                List<int> code = new List<int>() { indexStream[index + 1] };

                List<int> bufferWithCode = buffer.Concat(code).ToList();

                if (dictionary.Values.Any(x => x.SequenceEqual(bufferWithCode)))
                {
                    buffer = bufferWithCode;
                }
                else
                {
                    dictionary.Add(dictionary.Count, bufferWithCode);
                    codeStream.Add(GetCode(dictionary, buffer));
                    buffer = code;
                }
            }
            
            codeStream.Add(GetCode(dictionary, buffer));
            codeStream.Add(dictionary[tableSize + 1].First());

            return codeStream;
        }

        private static int GetCode(Dictionary<int, List<int>> dictionary, List<int> sequence)
        {
            return dictionary.Keys.FirstOrDefault(key => dictionary[key].SequenceEqual(sequence));
        }
    }
}
