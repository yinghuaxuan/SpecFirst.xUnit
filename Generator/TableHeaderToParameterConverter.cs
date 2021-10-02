namespace SpecFirst.TestGenerator.xUnit.Generator
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using SpecFirst.Core.DecisionTable;
    using SpecFirst.Core.NamingStrategy;

    public class TableHeaderToParameterConverter : ITableHeaderToParameterConverter
    {
        private readonly INamingStrategy _namingStrategy;

        public TableHeaderToParameterConverter(INamingStrategy namingStrategy)
        {
            _namingStrategy = namingStrategy;
        }

        public Parameter Convert(TableHeader tableHeader)
        {
            var sanitizedName = ReplaceIllegalCharacters(tableHeader.Name);
            var parameterName = _namingStrategy.Resolve(sanitizedName);

            return new Parameter
            {
                Type = CSharpTypeAlias.Alias(tableHeader.DataType),
                Name = parameterName,
                Direction = Map(tableHeader.TableHeaderType),
                Decoration = tableHeader.Decoration
            };
        }

        private string ReplaceIllegalCharacters(string input)
        {
            return Regex.Replace(input, @"[^\w@-]", "_");
        }

        private ParameterDirection Map(TableHeaderType tableHeaderType)
        {
            switch (tableHeaderType)
            {
                case TableHeaderType.Comment:
                    return ParameterDirection.Comment;
                case TableHeaderType.Input:
                    return ParameterDirection.Input;
                case TableHeaderType.Output:
                    return ParameterDirection.Output;
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
