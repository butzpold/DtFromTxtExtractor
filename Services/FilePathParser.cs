using DtFromTxtExtractor.Domains;

namespace DtFromTxtExtractor.Services
{
    internal class FilePathParser
    {
        public static FileMetaData Parse(
            string filePath, 
            bool containsheader = true
            )
        {            
            string? filename = Path.GetFileNameWithoutExtension(filePath);          
            
            return new FileMetaData(filePath, filename, containsheader);
        }
    }
}
