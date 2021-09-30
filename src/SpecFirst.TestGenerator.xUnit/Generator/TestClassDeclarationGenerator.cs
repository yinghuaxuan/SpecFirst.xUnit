namespace SpecFirst.TestGenerator.xUnit.Generator
{
    public class TestClassDeclarationGenerator
    {
        private readonly ITableNameToClassNameConverter _converter;

        public TestClassDeclarationGenerator(ITableNameToClassNameConverter converter)
        {
            _converter = converter;
        }

        public dynamic Convert(string tableName)
        {
            var data = _converter.Convert(tableName);

            return new {class_name = data};
        }
    }
}
