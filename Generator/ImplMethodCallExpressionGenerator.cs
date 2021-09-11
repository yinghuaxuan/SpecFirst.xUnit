namespace SpecFirst.TestGenerator.xUnit.Generator
{
    using System;
    using System.Linq;
    using HandlebarsDotNet;
    using SpecFirst.Core.DecisionTable;
    using SpecFirst.TestGenerator.xUnit.Template;

    public class ImplMethodCallExpressionGenerator
    {
        private readonly ITableHeaderToParameterConverter _parameterConverter;
        private readonly ITableNameToClassNameConverter _classNameConverter;

        public ImplMethodCallExpressionGenerator(ITableHeaderToParameterConverter parameterConverter, ITableNameToClassNameConverter classNameConverter)
        {
            _parameterConverter = parameterConverter;
            _classNameConverter = classNameConverter;
        }

        public string Convert(string tableName, TableHeader[] tableHeaders)
        {
            var parameters = tableHeaders.Select(h => _parameterConverter.Convert(h));
            var inputArgumentString = string.Join(", ", parameters.Where(p => p.Direction == ParameterDirection.Input).Select(p => p.Name));
            var outputTypeString = string.Join(", ", parameters.Where(p => p.Direction == ParameterDirection.Output).Select(p => $"{p.Type} {p.Name}_output"));
            outputTypeString = outputTypeString == string.Empty ? string.Empty : new string(outputTypeString.Prepend('(').Append(')').ToArray());

            Func<object, string> compiled = Handlebars.Compile(XUnitTemplate.IMPL_METHOD_CALL_EXPRESSION_TEMPLATE);

            return compiled(new
            {
                class_name = _classNameConverter.Convert(tableName),
                impl_return_values = outputTypeString,
                impl_arguments = inputArgumentString
            });
        }
    }
}
