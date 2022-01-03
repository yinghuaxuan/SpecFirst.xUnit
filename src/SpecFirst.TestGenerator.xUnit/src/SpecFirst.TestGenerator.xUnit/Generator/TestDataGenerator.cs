
namespace SpecFirst.TestGenerator.xUnit.Generator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using SpecFirst.Core.DecisionTable;
    using SpecFirst.Core.Serialization;

    public class TestDataGenerator
    {
        private readonly ISingularDataSerializer _singularDataSerializer;
        private readonly IArrayDataSerializer _arraySerializer;

        public TestDataGenerator(
            ISingularDataSerializer singularDataSerializer,
            IArrayDataSerializer arraySerializer)
        {
            _singularDataSerializer = singularDataSerializer;
            _arraySerializer = arraySerializer;
        }

        public dynamic Convert(DecisionTable table)
        {
            var testData = BuildTestData(table.TableHeaders, table.TableData);

            return new
            {
                test_data_and_comments = testData.Select(d => new { TestData = d.Item1, Comment = d.Item2 })
            };
        }

        private IEnumerable<(string, string)> BuildTestData(TableHeader[] tableHeaders, object?[,] decisionData)
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < decisionData.GetLength(0); i++)
            {
                builder.Clear();
                string comment = string.Empty;
                for (int j = 0; j < decisionData.GetLength(1); j++)
                {
                    if (tableHeaders[j].TableHeaderType == TableHeaderType.Comment)
                    {
                        comment = SanitizeString(decisionData[i, j]);
                    }
                    else
                    {
                        var data = Convert(decisionData, i, j, tableHeaders[j].DataType);
                        builder.Append($"{data}, ");
                    }
                }

                var dataString = builder.Remove(builder.Length - 2, 2).ToString();
                yield return (dataString, comment);
            }
        }

        private string Convert(object?[,] decisionData, int i, int j, Type dataType)
        {
            if (decisionData[i, j] == null)
            {
                return "null";
            }

            string data;
            if (decisionData[i, j].GetType().IsArray)
            {
                data = _arraySerializer.Serialize(decisionData[i, j], dataType);
            }
            else
            {
                data = _singularDataSerializer.Serialize(decisionData[i, j]);
            }

            return data;
        }

        private string SanitizeString(object? value)
        {
            if (value == null)
            {
                return string.Empty;
            }

            return value.ToString()
                .Replace("\n", " ")
                .Replace("\r", " ");
        }
    }
}
