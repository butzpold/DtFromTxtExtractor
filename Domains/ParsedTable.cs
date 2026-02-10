namespace DtFromTxtExtractor.Domains
{
    internal sealed class ParsedTable
    {
        public IReadOnlyList<string> Headers { get; }
        public IReadOnlyList<IReadOnlyList<string>> Rows { get; }
        public ParsedTable(
            IReadOnlyList<string> headers,
            IReadOnlyList<IReadOnlyList<string>> rows
            )
            {
            Headers = headers;
            Rows = rows;
            }
    }
}
