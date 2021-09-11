namespace SpecFirst.TestGenerator.xUnit.Generator
{
    using System;
    using HandlebarsDotNet;
    using SpecFirst.TestGenerator.xUnit.Template;

    public class TestClassDeclarationGenerator
    {
        private readonly ITableNameToClassNameConverter _converter;

        public TestClassDeclarationGenerator(ITableNameToClassNameConverter converter)
        {
            _converter = converter;
        }

        public string Convert(string tableName)
        {
            var data = _converter.Convert(tableName);

            Func<object, string> compiled = Handlebars.Compile(XUnitTemplate.TEST_NAME_TEMPLATE);

            return compiled(new {class_name = data});
        }
    }
}
