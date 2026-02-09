using DtFromTxtExtractor.Domains;
using System.Text;

namespace DtFromTxtExtractor.Services
{
    internal class PropertyCreator
    {
        public static ClassProperties Create(List<List<string>> DtAsList)        
        {           
            var properties = new List<Property>();
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
                // important for the object assembling later (type with/without?
                bool isNullable = columns[i].Any(string.IsNullOrWhiteSpace);

                // final assemblig of each property
                properties.Add(
                    new Property
                    {
                        PropertyName = headers[i],
                        PropertyType = inferredType,
                        TypeConfidence = confidence,
                        IsNullable = isNullable
                    }
                );
            }       

            return new ClassProperties
            {
                Properties = properties
            };
        }
    }
}
