namespace SpecFirst.TestGenerator.xUnit.Generator
{
    using System.Linq;
    using SpecFirst.Core.DecisionTable;

    public class TestMethodGenerator
    {
        private readonly ITableHeaderToParameterConverter _parameterConverter;
        private readonly ITableNameToClassNameConverter _classNameConverter;

        public TestMethodGenerator(ITableHeaderToParameterConverter parameterConverter, ITableNameToClassNameConverter classNameConverter)
        {
            _parameterConverter = parameterConverter;
            _classNameConverter = classNameConverter;
        }

        public dynamic Convert(DecisionTable table)
        {
            var parameters = table.TableHeaders.Select(h => _parameterConverter.Convert(h));
            var parameterString = string.Join(", ", parameters.Where(p => p.Direction != ParameterDirection.Comment).Select(p => p));

            return new
            {
                class_name = _classNameConverter.Convert(table.TableName),
                test_parameters = parameterString
            };
        }
    }
}
