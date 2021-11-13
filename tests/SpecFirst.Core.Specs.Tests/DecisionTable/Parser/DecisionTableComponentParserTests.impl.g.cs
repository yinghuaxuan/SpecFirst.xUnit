namespace SpecFirst.Core.Specs.Tests.DecisionTable.Parser.DecisionTableComponentParser
{
    using SpecFirst.Core.DecisionTable.Parser;
    using SpecFirst.Core.TypeResolver;
    using System;
    using System.Linq;
    using System.Xml.Linq;
    using SpecFirst.Core.DecisionTable;

    public partial class parse_decision_table_name_and_type
    {
        private partial (string, string) parse_decision_table_name_and_type_implementation(string decision_table)
        {
            var table = XElement.Parse(decision_table);
            var tableNameParser = new TableNameParser();
            var tableName = tableNameParser.Parse(table);
            var tableTypeParser = new TableTypeParser();
            var tableType = tableTypeParser.Parse(table);
            return (tableType.ToString(), tableName);
        }
    }

    public partial class parse_decision_table_headers
    {
        private partial (string, string, string) parse_decision_table_headers_implementation(string decision_table)
        {
            var parser = new TableHeadersParser();
            var tableHeaders = parser.Parse(XElement.Parse(decision_table));
            return
                (string.Join(",", tableHeaders.Where(h => h.TableHeaderType == TableHeaderType.Input).Select(h => h.Name)),
                    string.Join(",", tableHeaders.Where(h => h.TableHeaderType == TableHeaderType.Output).Select(h => h.Name)),
                    string.Join(",", tableHeaders.Where(h => h.TableHeaderType == TableHeaderType.Comment).Select(h => h.Name)));
        }
    }

    public partial class parse_header_decorations
    {
        private partial (object[], object[], object) parse_header_decorations_implementation(string decision_table)
        {
            var parser = new TableHeadersParser();
            var tableHeaders = parser.Parse(XElement.Parse(decision_table));
            var inputDecoration = tableHeaders.Where(h => h.TableHeaderType == TableHeaderType.Input).ElementAt(0).Decoration;
            var outputDecoration = tableHeaders.Where(h => h.TableHeaderType == TableHeaderType.Output).ElementAt(0).Decoration;
            var commentDecoration = tableHeaders.Where(h => h.TableHeaderType == TableHeaderType.Comment).ElementAt(0).Decoration;
            return
                (inputDecoration == null ? (Object[])null : inputDecoration.Split('|').Cast<object>().ToArray(),
                    outputDecoration == null ? (Object[])null : outputDecoration.Split('|').Cast<object>().ToArray(),
                    commentDecoration == null ? (Object[])null : commentDecoration.Split('|').Cast<object>().ToArray());

        }
    }

    public partial class parse_decision_table_data_types : decision_table_parser_base
    {
        private partial (string, string, string, string, string, string) parse_decision_table_data_types_implementation(string decision_table, int row_no)
        {
            var parser = new TableDataParser();
            var data = parser.Parse(XElement.Parse(decision_table), out var types);
            var row = GetRowData(data, row_no - 1);
            return
                (TypeHelper.GetTypeString(row[0].GetType()),
                    TypeHelper.GetTypeString(row[1].GetType()),
                    TypeHelper.GetTypeString(row[2].GetType()),
                    TypeHelper.GetTypeString(row[3].GetType()),
                    TypeHelper.GetTypeString(row[4].GetType()),
                    TypeHelper.GetTypeString(row[5].GetType()));
        }
    }

    public partial class parse_decision_table_data : decision_table_parser_base
    {
        private partial (string, int, decimal, double, bool, DateTime) parse_decision_table_data_implementation(string decision_table, int row_no)
        {
            var parser = new TableDataParser();
            var data = parser.Parse(XElement.Parse(decision_table), out var types);
            var row = GetRowData(data, row_no - 1);
            return
                (row[0]?.ToString(),
                    ((IntType)row[1]).ParsedValue,
                    ((DecimalType)row[2]).ParsedValue,
                    ((DoubleType)row[3]).ParsedValue,
                    (bool)row[4],
                    (DateTime)row[5]);
        }
    }

    public partial class parse_decision_table_data_types_with_variables : decision_table_parser_base
    {
        private partial (string, string, string) parse_decision_table_data_types_with_variables_implementation(string decision_table, int row_no)
        {
            return GetDecisionTableDataTypesByRow(decision_table, row_no);
        }
    }

    public partial class parse_decision_table_data_with_variables : decision_table_parser_base
    {
        private partial (string, object, object) parse_decision_table_data_with_variables_implementation(string decision_table, int row_no)
        {
            var row = GetDecisionTableDataByRow(decision_table, row_no);
            return (GetValue(row[0])?.ToString(), GetValue(row[1]), GetValue(row[2]));
        }
    }
}