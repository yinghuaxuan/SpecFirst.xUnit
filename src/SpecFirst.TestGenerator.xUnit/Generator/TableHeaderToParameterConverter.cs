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

        public IEnumerable<Parameter> Convert(TableHeader tableHeader)
        {
            var parameters = new List<Parameter>();

            var sanitizedName = ReplaceIllegalCharacters(tableHeader.Name);
            var parameterName = _namingStrategy.Resolve(sanitizedName);
            var parameterType = tableHeader.DataType;

            var parameter = new Parameter
            {
                Type = CSharpTypeAlias.Alias(parameterType),
                Name = parameterName,
                Direction = Map(tableHeader.TableHeaderType)
            };

            parameters.Add(parameter);

            if (!string.IsNullOrEmpty(tableHeader.Decoration))
            {
                var infoParameter = new Parameter
                {
                    Type = CSharpTypeAlias.Alias(typeof(string)),
                    Name = $"{parameterName}_decoration",
                    Direction = parameter.Direction
                };

                parameters.Add(infoParameter);
            }

            return parameters;
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
