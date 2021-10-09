namespace SpecFirst.Core.DecisionTable.Parser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using SpecFirst.Core.DecisionVariable.Validator;
    using SpecFirst.Core.TypeResolver;

    public sealed class TableDataParser
    {
        public object?[,] Parse(XElement decisionTable, out Type?[,] dataTypes)
        {
            IEnumerable<XElement> rows = decisionTable.Descendants("tr");
            XElement[] dataRows = rows.Skip(2).ToArray();
            string[,] data = GetDecisionData(dataRows);

            dataTypes = new Type?[data.GetLength(0), data.GetLength(1)];
            object?[,] values = new object?[data.GetLength(0), data.GetLength(1)];

            for (int i = 0; i < data.GetLength(0); i++)
            {
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    if (new DecisionVariableValidator().Validate(data[i, j], out var _))
                    {
                        dataTypes[i, j] = typeof(DecisionVariable.DecisionVariable);
                        values[i, j] = new DecisionVariable.DecisionVariable(data[i, j].TrimStart('$'), null, null);
                    }
                    else
                    {
                        Type? type = TypeResolver.Resolve(data[i, j], out object? value);
                        dataTypes[i, j] = type;
                        values[i, j] = value;
                    }
                }
            }

            return values;
        }

        private static string[,] GetDecisionData(XElement[] dataRows)
        {
            ITableDataNormalizer[] normalizers = {new ExcessiveSpaceNormalizer(), new EmptyToNullNormalizer()};
            int numberOfColumns = dataRows.First().Descendants("td").Count();
            string[,] data = new string[dataRows.Length, numberOfColumns];
            for (int i = 0; i < dataRows.Length; i++)
            {
                IEnumerable<XElement> columns = dataRows.ElementAt(i).Descendants("td").ToArray();
                for (int j = 0; j < numberOfColumns; j++)
                {
                    var value = columns.ElementAt(j).Value;
                    foreach (var n in normalizers) n.Normalize(ref value);
                    data[i, j] = value;
                }
            }

            return data;
        }
    }

    public interface ITableDataNormalizer
    {
        void Normalize(ref string value);
    }

    public class ExcessiveSpaceNormalizer : ITableDataNormalizer
    {
        public void Normalize(ref string value)
        {
            value = value.Trim();
        }
    }

    public class EmptyToNullNormalizer : ITableDataNormalizer
    {
        public void Normalize(ref string value)
        {
            value = string.IsNullOrEmpty(value) ? null : value;
        }
    }
}