using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtFromTxtExtractor.Services
{
    internal class ClassCreator
    {
        public static string Create(
            string className,
            List<List<string>> DtAsList
            )
        {            
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
                string type;
                // Nullable detection:
                // checks if there is at least one value in the column without a value 
                // important for the object assembling later (type with/without ?
                bool isNullable = columns[i].Any(string.IsNullOrWhiteSpace);

                columns[1].GetType();

                if (TypeConfidence.CheckConfidence(columns[i], v => int.TryParse(v, out _)))
                {
                    type = "int";
                }
                else if (TypeConfidence.CheckConfidence(columns[i], v => double.TryParse(v, out _)))
                { 
                    type = "double";
                }
                else if (TypeConfidence.CheckConfidence(columns[i], v => DateTime.TryParse(v, out _)))
                {
                    type = "DateTime";
                }
                else if (TypeConfidence.CheckConfidence(columns[i], v => bool.TryParse(v, out _)))
                {
                    type = "bool";
                }
                else
                {
                    type = "string";
                }
           
                if (isNullable && type != "string")
                {
                    type += "?";
                }

                if (type == "string")
                {
                    sb.AppendLine($"    public string? {headers[i]} {{ get; set; }} = \"\";");
                }
                else
                {
                    sb.AppendLine($"    public {type} {headers[i]} {{ get; set; }}");
                }                                                
            }

            sb.AppendLine("}");

            return sb.ToString();
        }
    }
}
