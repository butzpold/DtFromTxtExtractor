namespace DtFromTxtExtractor.Services
{
    internal class Extractor
    {
        public static List<List<string>> Extract(string filePath)
        {
            try
            {
                var rows = new List<List<string>>();
                var rowsUnparsed = new List<string>();
                
                var columnWidths = new List<int>{0};                
                using (var sr = new StreamReader(filePath))
                {
                    string? line;   // defines that line can be null
                    // read out the lines
                    while ((line = sr.ReadLine()) != null)
                    {
                        // exceptions which lines shouldn't stored
                        if (string.IsNullOrWhiteSpace (line))
                            continue;
                        if (line.StartsWith("-"))
                        {   
                            // ColumnMapping(indicator); indicator = "-"
                            for (int i = 0; i < line.Length-1; i++)
                            {
                                if (char.IsWhiteSpace(line[i]))
                                    columnWidths.Add(i);
                            }
                            continue;
                        }
                        rowsUnparsed.Add(line);                   
                    }
                    // Parse the Rows/Lines according to th columnWidths
                    foreach (var u in rowsUnparsed)
                    {
                        var rowParsed = new List<string>();
                        for (int i = 0; i < columnWidths.Count-1; i++)
                        {
                            var subtringStart = columnWidths[i];
                            var substringLength = columnWidths[i + 1] - columnWidths[i];
                            var rowValue = u.Substring(subtringStart, substringLength).Trim();    // .Trim() removes whitespace
                            rowParsed.Add(rowValue);
                        }
                        rows.Add(rowParsed);
                    }
                }                
                return rows;
            }
            catch (Exception ex)
            {
                return new List<List<string>> { new List<string>() };
            }
        }
    }
}
