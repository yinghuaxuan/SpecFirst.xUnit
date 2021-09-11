namespace SpecFirst.TestGenerator.xUnit.Generator
{
    using System;
    using System.Linq;
    using HandlebarsDotNet;
    using SpecFirst.Core.DecisionTable;
    using SpecFirst.TestGenerator.xUnit.Template;

    public class AssertStatementGenerator
    {
        private readonly ITableHeaderToParameterConverter _parameterConverter;

        public AssertStatementGenerator(ITableHeaderToParameterConverter parameterConverter)
        {
            _parameterConverter = parameterConverter;
        }

        public string Convert(TableHeader[] tableHeaders)
        {
            var parameters = tableHeaders.Select(h => _parameterConverter.Convert(h));
            var assertStatements = parameters
                .Where(p => p.Direction == ParameterDirection.Output)
                .Select(p => $"Assert.Equal({p.Name}_output, {p.Name})");

            Func<object, string> compiled = Handlebars.Compile(XUnitTemplate.ASSERT_STATEMENT_TEMPLATE);

            return compiled(new
            {
                assert_statements = assertStatements
            });
        }
    }
}
