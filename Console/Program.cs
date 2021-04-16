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
                args = new string[]{
                    "--source=cow.obj",
                    "--goal-format=gif",
                    "--output=newcow"
                };
                var value = KeyHandler.GetValues(args);
                ConversionFacade.InitiateConversion(value.source, value.destination, value.format);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
