using System.Text;
using DtFromTxtExtractor.Domains;

namespace DtFromTxtExtractor.Services
{
    internal static class ConfidenceReportUI
    {
        public static string Format(IEnumerable<ConfidencePropertyType> ConfidenceProperties)
        {
            var sb = new StringBuilder();

            foreach (var p in ConfidenceProperties)
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
