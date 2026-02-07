using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public static bool CheckConfidence(
            IEnumerable<string> columnValues,        
            Func<string, bool> tryParsePredicate,
            double threshold = 0.95
        )
        {
            return Level(columnValues, tryParsePredicate) >= threshold;
        }
}
}
