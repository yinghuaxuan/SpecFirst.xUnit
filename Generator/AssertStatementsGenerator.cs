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
            var parameters = tableHeaders.SelectMany(h => _parameterConverter.Convert(h));
            var assertStatements = parameters
                .Where(p => p.Direction == ParameterDirection.Output && !p.Name.EndsWith("_decoration"))
                .Select(p =>
                {
                    if (parameters.FirstOrDefault(m => m.Name == $"{p.Name}_decoration") != null)
                    {
                        return $"Assert.Equal({p.Name}_decoration({p.Name}_output), {p.Name}_decoration({p.Name}))";
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
