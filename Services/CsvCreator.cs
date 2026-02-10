using DtFromTxtExtractor.Domains;

namespace DtFromTxtExtractor.Services
{
    internal class CsvCreator
    {
        public static void Create(
            FileMetaData metaData,
            ParsedTable parsedTable,
            string separator = ";"
            )
        {
            var headers = parsedTable.Headers;
            var rows = parsedTable.Rows;
            using var sw = new StringWriter();
            if (metaData.ContainsHeader == true)
            {
                sw.WriteLine(
                    string.Join(separator, headers)
                    );
            }
            foreach (var r in rows)
            {
                sw.WriteLine(string.Join(separator, r));
            }
            File.WriteAllText(
                $"{metaData.FileName}.csv",
                sw.ToString()
                );            
        }
    }
}
