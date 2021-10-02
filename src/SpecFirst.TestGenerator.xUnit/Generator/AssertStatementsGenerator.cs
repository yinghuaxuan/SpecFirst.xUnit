namespace SpecFirst.TestGenerator.xUnit.Generator
{
    using System.Linq;
    using SpecFirst.Core.DecisionTable;

    public class AssertStatementsGenerator
    {
        private readonly ITableHeaderToParameterConverter _parameterConverter;

        public AssertStatementsGenerator(ITableHeaderToParameterConverter parameterConverter)
        {
            _parameterConverter = parameterConverter;
        }

        public dynamic Convert(TableHeader[] tableHeaders)
        {
            var parameters = tableHeaders.Select(h => _parameterConverter.Convert(h));
            var assertStatements = parameters
                .Where(p => p.Direction == ParameterDirection.Output)
                .Select(p =>
                {
                    if (!string.IsNullOrWhiteSpace(p.Decoration))
                    {
                        return $"Assert.Equal({p.Name}_decoration_implementation({p.Name}_output, {p.Name}_decoration), {p.Name}_decoration_implementation({p.Name}, {p.Name}_decoration))";
                    }

                    return $"Assert.Equal({p.Name}_output, {p.Name})";
                });

            return new
            {
                assert_statements = assertStatements
            };
        }
    }
}
