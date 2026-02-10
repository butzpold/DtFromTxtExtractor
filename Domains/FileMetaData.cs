namespace DtFromTxtExtractor.Domains
{
    internal class FileMetaData
    {
        public string FilePath { get; set; }    
        public string FileName { get; set; }
        public bool ContainsHeader { get; set; }

        public FileMetaData(string filePath, string fileName, bool containsHeader)
        {
            FilePath = filePath;
            FileName = fileName;
            ContainsHeader = containsHeader;
        }
    }
}
