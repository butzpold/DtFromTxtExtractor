namespace DtFromTxtExtractor.Domains
{
    internal class ConfidencePropertyType
    {
        public string PropertyName { get; set; } = "";
        public InferredType PropertyType { get; set; }
        public double TypeConfidence { get; set; }
    }
}
