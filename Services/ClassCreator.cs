using DtFromTxtExtractor.Domains;
using System.Text;

namespace DtFromTxtExtractor.Services
{
    internal class ClassCreator
    {
        private static readonly Dictionary<InferredType, string> TypeMap =
                new()
                {
                    [InferredType.String] = "string",
                    [InferredType.Int] = "int",
                    [InferredType.Double] = "double",
                    [InferredType.DateTime] = "DateTime",
                    [InferredType.Bool] = "bool"
                };
        public static CreateClassResult CreateClass(
            string className,
            List<List<string>> DtAsList
            )        
        {           
            var confidenceProperties = new List<ConfidencePropertyType>();
            // split the headers from the Rest of the Datalist
            var headers = DtAsList[0];
            var dataRows = DtAsList.Skip(1).ToList();

            var columns = new List<List<string>>();
            int columnCount = DtAsList[0].Count();
            // transpose the DataList; form rowise to columnwise
            for (int col = 0; col < columnCount; col++)
            {
                var column = new List<string>();
                foreach (var r in dataRows)
                {
                    column.Add(r[col]);
                }
                columns.Add(column);
            }
            // the actual Class Creator
            var sb = new StringBuilder();            
            sb.AppendLine("internal class " + className);
            sb.AppendLine("{");
            // assign Datatypes to columns and then merge the according Datatype and Header to an object
            // the next step won't change the type of the values of the columns. they remaining strings             
            for (int i = 0; i < columnCount; i++)
            {
                InferredType inferredType = InferredType.String;
                double confidence = 1.0;

                foreach (var rule in TypeInferenceRules.Rules)
                {
                    var result = rule.Check(columns[i]);

                    if (result.ValidConfidence)
                    {
                        inferredType = rule.Type;
                        confidence = result.Confidence;
                        break;
                    }
                }            

                // Nullable detection:
                // checks if there is at least one value in the column without a value 
                // important for the object assembling later (type with/without ?

                bool isNullable = columns[i].Any(string.IsNullOrWhiteSpace);
                string inferredTypeString = TypeMap[inferredType];
                
                if (isNullable && inferredType != InferredType.String)
                {
                    inferredTypeString += "?";
                }              

                if (inferredType == InferredType.String)
                {
                    sb.AppendLine($"    public string? {headers[i]} {{ get; set; }} = \"\";");
                }
                else
                {
                    sb.AppendLine($"    public {inferredTypeString} {headers[i]} {{ get; set; }}");
                }
                //
                confidenceProperties.Add(
                    new ConfidencePropertyType
                    {
                        PropertyName = headers[i],
                        PropertyType = inferredType,
                        TypeConfidence = confidence,
                        IsNullable = isNullable
                    }
                );
            }            

            sb.AppendLine("}");

            return new CreateClassResult
            {
                ClassCode = sb.ToString(),
                ConfidenceProperties = confidenceProperties
            };
        }
    }
}
