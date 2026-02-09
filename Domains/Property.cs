using DtFromTxtExtractor.Services;

namespace DtFromTxtExtractor.Domains
{
    internal class Property
    {
        public string PropertyName { get; set; } = "";
        public InferredType PropertyType { get; set; }
        public double TypeConfidence { get; set; }
        public bool IsNullable { get; set; }
    }
}
