using DtFromTxtExtractor.Services;

namespace DtFromTxtExtractor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("DtfromTxtExtractor");
            Console.WriteLine("------------------");
            Console.WriteLine(Environment.NewLine);
            
            Console.WriteLine("Enter file path below:");
            Console.Write("> ");

            int inputLeft = Console.CursorLeft;
            int inputTop = Console.CursorTop;
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("------------------");

            Console.SetCursorPosition(inputLeft, inputTop);
            // Example FilePath "WeatherStations_rtf.txt"
            string? filePath = Console.ReadLine();

            if (filePath != null) 
            {               
                Console.Clear();
                Console.WriteLine("DtfromTxtExtractor");
                Console.WriteLine("------------------");
                if (string.IsNullOrWhiteSpace(filePath))
                {
                    Console.WriteLine(Environment.NewLine);
                    Console.WriteLine("No file path provided.");
                    Console.WriteLine(Environment.NewLine);
                }
                else if (!File.Exists(filePath))
                {
                    Console.WriteLine(Environment.NewLine);
                    Console.WriteLine("File does not exist.");
                    Console.WriteLine(Environment.NewLine);
                }
                else
                {
                    var rows = Extractor.Extract(filePath);
                    var classCreator = ClassCreator.CreateClass("WeatherStation", rows);
                    var classCode = classCreator.ClassCode;
                    var confidenceProperties = classCreator.ConfidenceProperties;
                    Console.WriteLine(classCode);
                    Console.WriteLine("------------------");
                    Console.WriteLine("Type Confidences:");

                    //Console.WriteLine(ConfidenceReportFormatter.Format(result.ConfidenceProperties);
                    Console.WriteLine(ConfidenceReportUI.Format(confidenceProperties));
                }
                Console.WriteLine("------------------");
                Console.WriteLine("Press any Key to quit");
                Console.ReadKey();
            }

        }
    }
}

// Possible Future Improvements: 
// .UI namespace (Split ClassCreator and rename. Rename ConfidencePropertyType to Property and so on)
// finalize access modifiers (internal vs. public)
// Emit warnings for low confidence
// Make confidence thresholds configurable
