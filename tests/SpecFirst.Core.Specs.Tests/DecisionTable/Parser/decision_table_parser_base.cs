namespace SpecFirst.Core.Specs.Tests.DecisionTable.Parser.DecisionTableComponentParser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using SpecFirst.Core.DecisionTable.Parser;
    using SpecFirst.Core.DecisionVariable;
    using SpecFirst.Core.TypeResolver;

    public class decision_table_parser_base
    {
        protected object[] GetRowData(object[,] matrix, int rowNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(1))
                .Select(x => matrix[rowNumber, x])
                .ToArray();
        }

        protected IEnumerable<DecisionVariable> ParseDecisionVariables(string[] variables)
        {
            foreach (var variable in variables)
            {
                var splitedVariable = variable.Split(':');
                yield return new DecisionVariable(splitedVariable[0].Trim().TrimStart('$'), typeof(string), splitedVariable[1].Trim());
            }
        }

        protected (string, string, string) GetDecisionTableDataTypesByRow(string decision_table, int row_no)
        {
            var row = GetDecisionTableDataByRow(decision_table, row_no);
            return
                (TypeHelper.GetTypeString(GetType(row[0])),
                    TypeHelper.GetTypeString(GetType(row[1])),
                    TypeHelper.GetTypeString(GetType(row[2])));
        }

        protected (string, string, string) GetDecisionTableDataTypesByRow(string decision_table, string[] variables, int row_no)
        {
            DecisionTableParser parser = new DecisionTableParser();
            var decisionTable = parser.Parse(XElement.Parse(decision_table), ParseDecisionVariables(variables));
            if (row_no == 0)
            {
                return
                    (TypeHelper.GetTypeString(decisionTable.TableHeaders[0].DataType),
                        TypeHelper.GetTypeString(decisionTable.TableHeaders[1].DataType),
                        TypeHelper.GetTypeString(decisionTable.TableHeaders[2].DataType));
            }

            var row = GetRowData(decisionTable.TableData, row_no - 1);
            return
                (TypeHelper.GetTypeString(GetType(row[0], true)),
                    TypeHelper.GetTypeString(GetType(row[1], true)),
                    TypeHelper.GetTypeString(GetType(row[2], true)));
        }

        protected object[] GetDecisionTableDataByRow(string decision_table, int row_no)
        {
            var parser = new TableDataParser();
            var data = parser.Parse(XElement.Parse(decision_table), out var types);
            var row = GetRowData(data, row_no - 1);
            return row;
        }

        protected object[] GetDecisionTableDataByRow(string decision_table, string[] variables, int row_no)
        {
            DecisionTableParser parser = new DecisionTableParser();
            var decisionTable = parser.Parse(XElement.Parse(decision_table), ParseDecisionVariables(variables));
            var row = GetRowData(decisionTable.TableData, row_no - 1);
            return row;
        }

        private Type GetType(object value, bool realType = false)
        {
            if (realType && value is DecisionVariable v1)
            {
                return v1.Type;
            }

            return value.GetType();
        }

        protected object GetValue(object value)
        {
            if (value is DecisionVariable v1)
            {
                return v1.Value;
            }
            else if (value is IntType v2)
            {
                return v2.ParsedValue;
            }
            else if (value is DecimalType v3)
            {
                return v3.ParsedValue;
            }

            return value;
        }
    }
}