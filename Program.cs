using DtFromTxtExtractor.Services;

namespace DtFromTxtExtractor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("DtfromTxtExtractor");
            Console.WriteLine("------------");

            var filePath = "Example_WeatherStations.txt";
            
            var rows = Extractor.Extract(filePath);
            
            var classCode = ClassCreator.Create("WeatherStation", rows);
            
            Console.WriteLine(classCode);

            Console.WriteLine("------------");
            Console.WriteLine("Press any Key to quit");
            Console.ReadKey();
        }
    }
}

