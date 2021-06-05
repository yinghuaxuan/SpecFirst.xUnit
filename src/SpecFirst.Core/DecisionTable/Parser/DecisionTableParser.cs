namespace SpecFirst.Core.DecisionTable.Parser
{
    using SpecFirst.Core.TypeResolver;
    using System;
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Xml.Linq;

    public sealed class DecisionTableParser
    {
        private readonly TableNameParser _tableNameParser;
        private readonly TableHeadersParser _tableHeadersParser;
        private readonly TableDataParser _tableDataParser;

        public DecisionTableParser()
        {
            _tableNameParser = new TableNameParser();
            _tableHeadersParser = new TableHeadersParser();
            _tableDataParser = new TableDataParser();
        }

        public DecisionTable Parse(XElement table)
        {
            var tableName = _tableNameParser.Parse(table);
            var tableHeaders = _tableHeadersParser.Parse(table).ToArray();
            object[,] tableData = _tableDataParser.Parse(table, out Type[,] dataTypes);
            UpdateColumnTypesFromData(tableHeaders, dataTypes);
            var variables = ExtractDecisionVariables(tableData, tableHeaders);
            return new DecisionTable(tableName, tableHeaders, tableData, variables);
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

        private static DecisionVariable.DecisionVariable[] ExtractDecisionVariables(object[,] tableData, TableHeader[] tableHeaders)
        {
            var variables = new ConcurrentDictionary<string, DecisionVariable.DecisionVariable>();
            for (int i = 0; i < tableData.GetLength(0); i++)
            {
                for (int j = 0; j < tableData.GetLength(1); j++)
                {
                    if (tableData[i, j] is DecisionVariable.DecisionVariable variable)
                    {
                        variables.AddOrUpdate(
                            variable.Name,
                            variable,
                            (key, value) => { variable.AssociatedTableHeader.Add(tableHeaders[j]); return variable; });
                    }
                }
            }

            return variables.Values.ToArray();
        }
    }
} 