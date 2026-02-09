using DtFromTxtExtractor.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtFromTxtExtractor.UI
{
    internal class MainUI
    {
        public static void Run(string[] args)
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
                    var properties = PropertyCreator.Create(rows);
                    Console.WriteLine(ClassCodeUI.Format(filePath, properties));
                    Console.WriteLine("------------------");
                    Console.WriteLine("Type Confidences:");

                    Console.WriteLine(ConfidenceUI.Format(properties));
                }
                Console.WriteLine("------------------");
                Console.WriteLine("Press any Key to quit");
                Console.ReadKey();
            }
        }
    }
}
