namespace DtFromTxtExtractor.Domains
{    internal class CreateClassResult
    {
        public string? ClassCode { get; set; }
        public List<ConfidencePropertyType> ConfidenceProperties { get; set; } = [];
    }
}