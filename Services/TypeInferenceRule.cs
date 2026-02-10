// stored function pointer
// function inside each inference rule referring TypeInferenceRules.cs
// calls indirectly TypeConfidenc.CheckConfidence(col)

using DtFromTxtExtractor.Domains;

namespace DtFromTxtExtractor.Services
{
    internal record TypeInferenceRule //record class just holding data, not behavior
    (
        InferredType Type,
        Func<IEnumerable<string>, CheckConfidenceResult> Check
        );
    
}
// Shortcut roughly for:
/*
internal class TypeInferenceRule
{
    public InferredType Type { get; }
    public Func<IEnumerable<string>, CheckConfidenceResult> Check { get; }

    public TypeInferenceRule(
        InferredType type,
        Func<IEnumerable<string>, CheckConfidenceResult> check)
    {
        Type = type;
        Check = check;
    }
}
*/
