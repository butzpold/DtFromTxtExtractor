using System.Text;
using DtFromTxtExtractor.Domains;

namespace DtFromTxtExtractor.UI
{
    internal static class ConfidenceUI
    {
        public static string Format(ClassProperties classProperties)
        {
            var sb = new StringBuilder();

            foreach (var p in classProperties.Properties)
            {
                sb.AppendLine(
                    $"{p.PropertyName,-20} | " +
                    $"{p.PropertyType,-10} | " +
                    $"Confidence: {p.TypeConfidence:P1}"
                );
            }

            return sb.ToString();
        }
    }
}
