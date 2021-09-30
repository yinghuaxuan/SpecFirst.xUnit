
namespace SpecFirst.TestGenerator.xUnit.Generator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using SpecFirst.Core.DecisionTable;
    using SpecFirst.Core.DecisionVariable;
    using SpecFirst.Core.Serialization;

    public class TestDataGenerator
    {
        private readonly IPrimitiveDataSerializer _primitiveDataSerializer;
        private readonly IArrayDataSerializer _arraySerializer;
        private readonly ITableVariableSerializer _variableSerializer;

        public TestDataGenerator(
            IPrimitiveDataSerializer primitiveDataSerializer,
            IArrayDataSerializer arraySerializer,
            ITableVariableSerializer variableSerializer)
        {
            _primitiveDataSerializer = primitiveDataSerializer;
            _arraySerializer = arraySerializer;
            _variableSerializer = variableSerializer;
        }

        public dynamic Convert(TableHeader[] tableHeaders, object[,] decisionData)
        {
            var testData = BuildTestData(tableHeaders, decisionData);

            return new
            {
                test_data_and_comments = testData.Select(d => new {TestData = d.Item1, Comment = d.Item2})
            };

        }

        private List<(string, string)> BuildTestData(TableHeader[] tableHeaders, object[,] decisionData)
        {
            List<(string, string)> testData = new List<(string, string)>();
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < decisionData.GetLength(0); i++)
            {
                builder.Clear();
                string comment = null;
                for (int j = 0; j < decisionData.GetLength(1); j++)
                {
                    if (tableHeaders[j].TableHeaderType == TableHeaderType.Comment)
                    {
                        comment = SanitizeString(decisionData[i, j].ToString());
                    }
                    else
                    {
                        var data = Convert(decisionData, i, j, tableHeaders[j].DataType);
                        builder.Append($"{data}, ");
                    }
                }

                var item = builder.Remove(builder.Length - 2, 2).ToString();

                testData.Add((item, comment));
            }

            return testData;
        }

        private string Convert(object[,] decisionData, int i, int j, Type dataType)
        {
            string data;
            if (decisionData[i, j].GetType().IsArray)
            {
                data = _arraySerializer.Serialize(decisionData[i, j], dataType);
            }
            else if (decisionData[i, j] is DecisionVariable)
            {
                data = _variableSerializer.Serialize(decisionData[i, j]);
            }
            else
            {
                data = _primitiveDataSerializer.Serialize(decisionData[i, j]);
            }

            return data;
        }

        private string SanitizeString(string value)
        {
            return value
                .Replace("\n", " ")
                .Replace("\r", " ");
        }
    }
}
