namespace SpecFirst.Core.Specs.Tests.DecisionTable.Parser.DecisionTableParser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using SpecFirst.Core.DecisionTable;
    using SpecFirst.Core.DecisionTable.Parser;
    using SpecFirst.Core.DecisionVariable;
    using SpecFirst.Core.Specs.Tests.DecisionTable.Parser.DecisionTableComponentParser;
    using SpecFirst.Core.TypeResolver;

    public partial class parse_decision_table
    {
        private partial (string, string, string, string, string) parse_decision_table_implementation(string decision_table)
        {
            DecisionTableParser parser = new DecisionTableParser();
            var decisionTable = parser.Parse(XElement.Parse(decision_table), new List<DecisionVariable>());
            return
                (decisionTable.TableType.ToString(),
                decisionTable.TableName,
                string.Join(",", decisionTable.TableHeaders.Where(h => h.TableHeaderType == TableHeaderType.Input).Select(h => h.Name)),
                string.Join(",", decisionTable.TableHeaders.Where(h => h.TableHeaderType == TableHeaderType.Output).Select(h => h.Name)),
                string.Join(",", decisionTable.TableHeaders.Where(h => h.TableHeaderType == TableHeaderType.Comment).Select(h => h.Name)));
        }
    }

    public partial class parse_decision_table_data_types : decision_table_parser_base
    {
        private partial (string, string, string, string, string, string) parse_decision_table_data_types_implementation(string decision_table, int row_no)
        {
            DecisionTableParser parser = new DecisionTableParser();
            var decisionTable = parser.Parse(XElement.Parse(decision_table), new List<DecisionVariable>());
            if (row_no == 0)
            {
                return
                    (TypeHelper.GetTypeString(decisionTable.TableHeaders[0].DataType),
                        TypeHelper.GetTypeString(decisionTable.TableHeaders[1].DataType),
                        TypeHelper.GetTypeString(decisionTable.TableHeaders[2].DataType),
                        TypeHelper.GetTypeString(decisionTable.TableHeaders[3].DataType),
                        TypeHelper.GetTypeString(decisionTable.TableHeaders[4].DataType),
                        TypeHelper.GetTypeString(decisionTable.TableHeaders[5].DataType));
            }

            var row = GetRowData(decisionTable.TableData, row_no - 1);
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
            DecisionTableParser parser = new DecisionTableParser();
            var decisionTable = parser.Parse(XElement.Parse(decision_table), new List<DecisionVariable>());
            var row = GetRowData(decisionTable.TableData, row_no - 1);
            return
                (row[0].ToString(),
                    ((IntType)row[1]).ParsedValue,
                    ((DecimalType)row[2]).ParsedValue,
                    ((DoubleType)row[3]).ParsedValue,
                    (bool)row[4],
                    (DateTime)row[5]);
        }
    }

    public partial class parse_decision_table_data_types_with_variables : decision_table_parser_base
    {
        private partial (string, string, string) parse_decision_table_data_types_with_variables_implementation(string decision_table, string[] variables, int row_no)
        {
            return GetDecisionTableDataTypesByRow(decision_table, variables, row_no);
        }
    }

    public partial class parse_decision_table_data_with_variables : decision_table_parser_base
    {
        private partial (string, object, object) parse_decision_table_data_with_variables_implementation(string decision_table, string[] variables, int row_no)
        {
            var row = GetDecisionTableDataByRow(decision_table, variables, row_no);
            return (GetValue(row[0]).ToString(), GetValue(row[1]), GetValue(row[2]));
        }
    }

    public partial class parse_decision_table_data_types_with_variables_not_defined : decision_table_parser_base
    {
        private partial (string, string, string) parse_decision_table_data_types_with_variables_not_defined_implementation(string decision_table, string[] variables, int row_no)
        {
            return GetDecisionTableDataTypesByRow(decision_table, variables, row_no);
        }
    }

    public partial class parse_decision_table_data_with_variables_not_defined : decision_table_parser_base
    {
        private partial (string, object, object) parse_decision_table_data_with_variables_not_defined_implementation(string decision_table, string[] variables, int row_no)
        {
            var row = GetDecisionTableDataByRow(decision_table, variables, row_no);
            return (GetValue(row[0]).ToString(), GetValue(row[1]), GetValue(row[2]));
        }
    }

    public partial class parse_setup_decision_table_data_types_with_variables : decision_table_parser_base
    {
        private partial (string, string, string) parse_setup_decision_table_data_types_with_variables_implementation(string decision_table, string[] variables, int row_no)
        {
            return GetDecisionTableDataTypesByRow(decision_table, variables, row_no);
        }
    }

    public partial class parse_setup_decision_table_data_with_variables : decision_table_parser_base
    {
        private partial (string, object, object) parse_setup_decision_table_data_with_variables_implementation(string decision_table, string[] variables, int row_no)
        {
            var row = GetDecisionTableDataByRow(decision_table, variables, row_no);
            return (GetValue(row[0]).ToString(), GetValue(row[1]), GetValue(row[2]));
        }
    }

}