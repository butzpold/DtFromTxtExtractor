using DtFromTxtExtractor.Domains;

namespace DtFromTxtExtractor.Services
{
    internal class FixedWidthTxtParser
    {
        public static ParsedTable Parse(FileMetaData metaData)
        {
            var rowsUnparsed = new List<string>();
            var headersList = new List<string>();
            var rows = new List<List<string>>();                
            var columnWidths = new List<int>{0};                
                
            using (var sr = new StreamReader(metaData.FilePath))
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
                // and header is true/false
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
                if (metaData.ContainsHeader == true)
                {
                    headersList = rows[0];
                    rows.RemoveAt(0);
                }
            }
            return new ParsedTable(headersList, rows);
        }
    }
}
