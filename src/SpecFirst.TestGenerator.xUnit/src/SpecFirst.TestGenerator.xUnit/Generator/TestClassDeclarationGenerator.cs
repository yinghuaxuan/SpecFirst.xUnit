namespace SpecFirst.TestGenerator.xUnit.Generator
{
    using SpecFirst.Core.DecisionTable;

    public class TestClassDeclarationGenerator
    {
        private readonly ITableNameToClassNameConverter _converter;

        public TestClassDeclarationGenerator(ITableNameToClassNameConverter converter)
        {
            _converter = converter;
        }

        public dynamic Convert(DecisionTable table)
        {
            var data = _converter.Convert(table.TableName);

            return new {class_name = data};
        }
    }
}
