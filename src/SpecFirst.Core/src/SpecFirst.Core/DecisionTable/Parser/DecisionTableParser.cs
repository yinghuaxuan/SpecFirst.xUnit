namespace SpecFirst.Core.DecisionTable.Parser
{
    using SpecFirst.Core.TypeResolver;
    using SpecFirst.Core.DecisionVariable;
    using System;
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

        public DecisionTable Parse(XElement table, IList<DecisionVariable> decisionVariables)
        {
            var tableType = _tableTypeParser.Parse(table);
            var tableName = _tableNameParser.Parse(table);
            var tableHeaders = _tableHeadersParser.Parse(table).ToArray();
            object?[,] tableData = _tableDataParser.Parse(table, out Type?[,] dataTypes);
            var variables = ParseDecisionVariablesFromTable(tableName, tableData, tableType, tableHeaders, decisionVariables);
            ReplaceVariableTypesWithRealTypes(dataTypes, tableData, variables);
            UpdateColumnTypesFromData(tableHeaders, dataTypes);
            return new DecisionTable(tableType, tableName, tableHeaders, tableData, variables);
        }

        private void ReplaceVariableTypesWithRealTypes(Type?[,] dataTypes, object?[,] tableData, DecisionVariable[] variables)
        {
            for (int i = 0; i < tableData.GetLength(0); i++)
            {
                for (int j = 0; j < tableData.GetLength(1); j++)
                {
                    if (dataTypes[i, j] == typeof(DecisionVariable))
                    {
                        dataTypes[i, j] = (tableData[i, j] as DecisionVariable).Type!; // replace with the real type of the data
                    }
                    else if (dataTypes[i, j] == typeof(DecisionVariable[]))
                    {
                        var variables_ = (tableData[i, j] as object[]).Cast<DecisionVariable>().ToArray();
                        var type = variables_[0].Type;
                        for (int k = 1; k < variables_.Length; k++)
                        {
                            if (variables_[k].Type != type)
                            {
                                type = typeof(object);
                                break;
                            }
                        }
                        dataTypes[i, j] = type.MakeArrayType();
                    }
                }
            }
        }

        private static void UpdateColumnTypesFromData(TableHeader[] headers, Type?[,] dataTypes)
        {
            var types = ResolveColumnTypes(dataTypes);
            for (int i = 0; i < headers.Count(); i++)
            {
                headers[i].UpdateDataType(types.ElementAt(i));
            }
        }

        private static IEnumerable<Type> ResolveColumnTypes(Type?[,] dataTypes)
        {
            var columns = dataTypes.GetLength(1);
            for (int i = 0; i < columns; i++)
            {
                var columnTypes = ExtractColumnTypes(dataTypes, i).ToArray();
                yield return CollectionTypeResolver.Resolve(columnTypes);
            }
        }

        private static IEnumerable<Type?> ExtractColumnTypes(Type?[,] dataTypes, int column)
        {
            var rows = dataTypes.GetLength(0);
            for (int i = 0; i < rows; ++i)
            {
                yield return dataTypes[i, column];
            }
        }

        private static DecisionVariable[] ParseDecisionVariablesFromTable(
            string tableName,
            object?[,] tableData,
            TableType tableType,
            TableHeader[] tableHeaders,
            IList<DecisionVariable> definedVariables)
        {
            var variables = ExtractDecisionVariablesFromTable(tableData, tableHeaders);
            foreach (var variable in variables)
            {
                if (tableType == TableType.Setup)
                {
                    var header = variable.AssociatedTableHeaders.Where(h => h.TableHeaderType == TableHeaderType.Output);
                    if (header.Any())
                    {
                        UpdateNewVariable(definedVariables, variable, tableName);
                        continue;
                    }
                }

                UpdateExistingVariable(definedVariables, variable);
            }

            return variables.ToArray();
        }

        private static void UpdateNewVariable(IList<DecisionVariable> definedVariables, DecisionVariable variable, string tableName)
        {
            var temp = definedVariables.FirstOrDefault(v => v == variable);
            if (temp != null)
            {
                throw new InvalidOperationException($"{variable.Name} variable is assigned as an output but it is already defined");
            }

            variable.Type = typeof(object);
            variable.SourceTable = tableName;
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

        private static IEnumerable<DecisionVariable> ExtractDecisionVariablesFromTable(object?[,] tableData, TableHeader[] tableHeaders)
        {
            var variables = new Dictionary<string, DecisionVariable>();

            for (int i = 0; i < tableData.GetLength(0); i++)
            {
                for (int j = 0; j < tableData.GetLength(1); j++)
                {
                    if (tableData[i, j] is DecisionVariable variable)
                    {
                        tableData[i, j] = ExtractDecisionVariable(tableHeaders, variables, i, j, variable);
                    }
                    else if (tableData[i, j] != null && tableData[i, j].GetType().IsArray)
                    {
                        var values = tableData[i, j] as object[];
                        if (values != null)
                        {
                            for (int k = 0; k < values.Count(); k++)
                            {
                                if (values[k] is DecisionVariable v)
                                {
                                    values[k] = ExtractDecisionVariable(tableHeaders, variables, i, j, v);
                                }
                            }
                        }
                    }
                }
            }

            return variables.Values;

            static DecisionVariable ExtractDecisionVariable(TableHeader[] tableHeaders, Dictionary<string, DecisionVariable> variables, int i, int j, DecisionVariable variable)
            {
                variable.AssociatedTableHeaders.Add(tableHeaders[j]);
                if (!variables.ContainsKey(variable.Name))
                {
                    variables.Add(variable.Name, variable);
                }

                return variables[variable.Name];
            }
        }
    }
}