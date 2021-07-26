namespace SpecFirst.Core.DecisionTable.Parser
{
    using SpecFirst.Core.TypeResolver;
    using SpecFirst.Core.DecisionVariable;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    public sealed class DecisionTableParser : IDecisionTableParser
    {
        private readonly TableTypeParser _tableTypeParser;
        private readonly TableNameParser _tableNameParser;
        private readonly TableHeadersParser _tableHeadersParser;
        private readonly TableDataParser _tableDataParser;

        public DecisionTableParser()
        {
            _tableTypeParser = new TableTypeParser();
            _tableNameParser = new TableNameParser();
            _tableHeadersParser = new TableHeadersParser();
            _tableDataParser = new TableDataParser();
        }

        public DecisionTable Parse(XElement table, IEnumerable<DecisionVariable> decisionVariables)
        {
            var tableType = _tableTypeParser.Parse(table);
            var tableName = _tableNameParser.Parse(table);
            var tableHeaders = _tableHeadersParser.Parse(table).ToArray();
            object[,] tableData = _tableDataParser.Parse(table, out Type[,] dataTypes);
            var variables = ParseDecisionVariables(tableData, tableType, tableHeaders, decisionVariables.ToList());
            ReplaceDataTypesWithRealVariableTypes(dataTypes, tableData, variables);
            UpdateColumnTypesFromData(tableHeaders, dataTypes);
            return new DecisionTable(tableType, tableName, tableHeaders, tableData, variables);
        }

        private void ReplaceDataTypesWithRealVariableTypes(Type[,] dataTypes, object[,] tableData, DecisionVariable[] variables)
        {
            for (int i = 0; i < tableData.GetLength(0); i++)
            {
                for (int j = 0; j < tableData.GetLength(1); j++)
                {
                    if (tableData[i, j] is DecisionVariable variable)
                    {
                        var result = variables.First(v => v == variable);
                        dataTypes[i, j] = result.Type!; // replace with the real type of the data
                    }
                }
            }
        }

        private static void UpdateColumnTypesFromData(TableHeader[] headers, Type[,] dataTypes)
        {
            var types = ResolveColumnTypes(dataTypes);
            for (int i = 0; i < headers.Count(); i++)
            {
                headers[i].UpdateDataType(types[i]);
            }
        }

        private static Type[] ResolveColumnTypes(Type[,] dataTypes)
        {
            var columns = dataTypes.GetLength(1);
            var columnTypes = new Type[columns];
            for (int i = 0; i < columns; i++)
            {
                columnTypes[i] = CollectionTypeResolver.Resolve(ExtractColumnTypes(dataTypes, i));
            }

            return columnTypes;
        }

        private static Type[] ExtractColumnTypes(Type[,] dataTypes, int column)
        {
            var rows = dataTypes.GetLength(0);
            var array = new Type[rows];
            for (int i = 0; i < rows; ++i)
            {
                array[i] = dataTypes[i, column];
            }
            return array;
        }

        private static DecisionVariable[] ParseDecisionVariables(
            object[,] tableData,
            TableType tableType,
            TableHeader[] tableHeaders,
            IList<DecisionVariable> definedVariables)
        {
            var variables = ExtractDecisionVariables(tableData, tableHeaders);
            foreach (var variable in variables)
            {
                if (tableType == TableType.Setup)
                {
                    var header = variable.AssociatedTableHeaders.Where(h => h.TableHeaderType == TableHeaderType.Output);
                    if (header.Any())
                    {
                        UpdateNewVariable(definedVariables, variable);
                        continue;
                    }
                }

                UpdateExistingVariable(definedVariables, variable);
            }

            return variables.ToArray();
        }

        private static void UpdateNewVariable(IList<DecisionVariable> definedVariables, DecisionVariable variable)
        {
            var temp = definedVariables.FirstOrDefault(v => v == variable);
            if (temp != null)
            {
                // throw new InvalidOperationException($"{variable.Name} variable is assigned as an output but it is already defined");
            }

            variable.Type = typeof(object);
            definedVariables.Add(variable);
        }

        private static void UpdateExistingVariable(IEnumerable<DecisionVariable> definedVariables, DecisionVariable variable)
        {
            var temp = definedVariables.FirstOrDefault(v => v == variable);
            if (temp == null)
            {
                variable.Type = typeof(string);
                variable.Value = $"${variable.Name}";
            }
            else
            {
                variable.Type = temp.Type;
                variable.Value = temp.Value;
            }
        }

        private static IEnumerable<DecisionVariable> ExtractDecisionVariables(object[,] tableData, TableHeader[] tableHeaders)
        {
            var variables = new Dictionary<string, DecisionVariable>();

            for (int i = 0; i < tableData.GetLength(0); i++)
            {
                for (int j = 0; j < tableData.GetLength(1); j++)
                {
                    if (tableData[i, j] is DecisionVariable variable)
                    {
                        variable.AssociatedTableHeaders.Add(tableHeaders[j]);
                        if (!variables.ContainsKey(variable.Name))
                        {
                            variables.Add(variable.Name, variable);
                        }

                        tableData[i, j] = variables[variable.Name];
                    }
                }
            }

            return variables.Values;
        }
    }
}