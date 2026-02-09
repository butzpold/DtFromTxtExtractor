namespace DtFromTxtExtractor.Services
{
    internal static class TypeConfidence
    {
        public static double Level(
            IEnumerable<string> columnValues,
            // tryParsePredicate looks like v => type.TryParse(v, out _); type = int/bool/strin/DateTime etc.
            Func<string, bool> tryParsePredicate,
            double threshold = 0.95         
            )
        {
            var data = columnValues
                .Where(v => !string.IsNullOrWhiteSpace(v))
                .ToList();

            if (data.Count == 0) 
                return 0.0;

            int validTypeCount = data.Count(tryParsePredicate);

            return (double)validTypeCount / data.Count;
        }        
        // gives an object with two properties back:
        //  a double of the confidence Level of the column and a bool if this is higher than the threshold        
        public static CheckConfidenceResult CheckConfidence(
            IEnumerable<string> columnValues,        
            Func<string, bool> tryParsePredicate,
            double threshold = 0.95
        )
        {
            double confidence = Level(columnValues, tryParsePredicate);       

            return new CheckConfidenceResult
            {
                Confidence = confidence,
                ValidConfidence = confidence >= threshold
            };
        }
}
}
