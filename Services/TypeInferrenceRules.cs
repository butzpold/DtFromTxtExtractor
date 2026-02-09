using DtFromTxtExtractor.Domains;

namespace DtFromTxtExtractor.Services
{
    internal static class TypeInferenceRules
    {
        public static IReadOnlyList<TypeInferenceRule> Rules { get; } =
            new List<TypeInferenceRule>
            {
                new
                (
                    InferredType.Int,
                    col => TypeConfidence.CheckConfidence(col, v => int.TryParse(v, out _))
                ),
                new
                (
                    InferredType.Double,
                    col => TypeConfidence.CheckConfidence(col ,v => double.TryParse(v, out _))
                ),
                new
                (
                    InferredType.DateTime,
                    col => TypeConfidence.CheckConfidence(col, v => DateTime.TryParse(v, out _))
                ),
                new
                (
                    InferredType.Bool,
                    col => TypeConfidence.CheckConfidence(col, v => bool.TryParse(v, out _))
                )
            };
    }
}
