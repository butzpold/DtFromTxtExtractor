using DtFromTxtExtractor.Services;

namespace DtFromTxtExtractor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("DtfromTxtExtractor");
            Console.WriteLine("------------");

            var filePath = "WeatherStations_rtf.txt";
            
            var rows = Extractor.Extract(filePath);

            var classCreator = ClassCreator.CreateClass("WeatherStation", rows);
            var classCode = classCreator.ClassCode;
            var confidenceProperties = classCreator.ConfidenceProperties;

            Console.WriteLine(classCode);
            Console.WriteLine(confidenceProperties); //in need of method to read

            Console.WriteLine("------------");
            Console.WriteLine("Press any Key to quit");
            Console.ReadKey();
        }
    }
}

// Show the ClassCreator.CreateClass.ConfidenceProperties adiquate
// Emit warnings for low confidence
// Make confidence thresholds configurable
