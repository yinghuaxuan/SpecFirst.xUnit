namespace SpecFirst.TestGenerator.xUnit.Generator
{
    using System.Linq;
    using SpecFirst.Core.DecisionTable;

    public class TestMethodGenerator
    {
        private readonly ITableHeaderToParameterConverter _parameterConverter;

        public TestMethodGenerator(ITableHeaderToParameterConverter parameterConverter)
        {
            _parameterConverter = parameterConverter;
        }

        public dynamic Convert(string tableName, TableHeader[] tableHeaders)
        {
            var parameters = tableHeaders.SelectMany(h => _parameterConverter.Convert(h));
            var parameterString = string.Join(", ", parameters.Where(p => p.Direction != ParameterDirection.Comment).Select(p => p));

            return new
            {
                test_parameters = parameterString
            };
        }
    }
}
