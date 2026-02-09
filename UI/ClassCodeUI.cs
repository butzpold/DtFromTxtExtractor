using System.Text;
using System.IO;
using DtFromTxtExtractor.Domains;

namespace DtFromTxtExtractor.UI
{
    internal static class ClassCodeUI
    {
        // inventing a dictionary to get the according PropertyTypes
        // in string for the assembling
        private static readonly Dictionary<InferredType, string> TypeMap =
                new()
                {
                    [InferredType.String] = "string",
                    [InferredType.Int] = "int",
                    [InferredType.Double] = "double",
                    [InferredType.DateTime] = "DateTime",
                    [InferredType.Bool] = "bool"
                };
        public static string Format(string filePath, ClassProperties classProperties)
        {   
            // getting the ClassName out of the FilePath
            string? className = Path.GetFileNameWithoutExtension(filePath);
            // assembling the ClassProperties in string-Format
            var sb = new StringBuilder();
            string inferredPropertyTypeAsString;
            sb.AppendLine("internal class " + className);
            sb.AppendLine("{");

            foreach (var p in classProperties.Properties)
            {
                inferredPropertyTypeAsString = TypeMap[p.PropertyType];

                if (p.IsNullable && p.PropertyType != InferredType.String)
                {
                    inferredPropertyTypeAsString += "?";
                }

                if (p.PropertyType == InferredType.String)
                {
                    sb.AppendLine($"    public string? {p.PropertyName} {{ get; set; }} = \"\";");
                }
                else
                {
                    sb.AppendLine($"    public {inferredPropertyTypeAsString} {p.PropertyName} {{ get; set; }}");
                }
            }
            
            sb.AppendLine("}");
            
            return sb.ToString();
        }
    }
}
