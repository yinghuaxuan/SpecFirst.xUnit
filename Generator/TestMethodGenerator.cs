namespace SpecFirst.TestGenerator.xUnit.Generator
{
    using System;
    using System.Linq;
    using HandlebarsDotNet;
    using SpecFirst.Core.DecisionTable;
    using SpecFirst.TestGenerator.xUnit.Template;

    public class TestMethodGenerator
    {
        private readonly ITableHeaderToParameterConverter _parameterConverter;
        private readonly ITableNameToClassNameConverter _classNameConverter;

        public TestMethodGenerator(ITableHeaderToParameterConverter parameterConverter, ITableNameToClassNameConverter classNameConverter)
        {
            _parameterConverter = parameterConverter;
            _classNameConverter = classNameConverter;
        }

        public string Convert(string tableName, TableHeader[] tableHeaders)
        {
            var parameters = tableHeaders.Select(h => _parameterConverter.Convert(h));
            var parameterString = string.Join(", ", parameters.Where(p => p.Direction != ParameterDirection.Comment).Select(p => p));

            Func<object, string> compiled = Handlebars.Compile(XUnitTemplate.TEST_METHOD_TEMPLATE);

            return compiled(new
            {
                class_name = _classNameConverter.Convert(tableName),
                test_parameters = parameterString
            });
        }
    }
}
