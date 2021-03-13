using System;
using Converter;

namespace ConsoleInterface
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var value = KeyHandler.GetValues(args);
                ConversionFacade.InitiateConversion(value.source, value.destination, value.format);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
