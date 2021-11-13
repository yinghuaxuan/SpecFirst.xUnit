namespace SpecFirst.TestGenerator.xUnit
{
    using System;
    using System.Collections.Generic;
    using HandlebarsDotNet;
    using SpecFirst.Core;
    using SpecFirst.Core.DecisionTable;
    using SpecFirst.Core.Setting;
    using SpecFirst.TestGenerator.xUnit.Template;

    public class XUnitTestsGenerator : ITestsGenerator
    {
        private readonly XUnitTemplateDataProvider _templateDataProvider;

        public XUnitTestsGenerator()
        {
            _templateDataProvider = new XUnitTemplateDataProvider();
        }

        public IEnumerable<string> Generate(
            SpecFirstSettings settings,
            IEnumerable<DecisionTable> decisionTables)
        {
            object[] templateData = _templateDataProvider.GetTemplateData(decisionTables);
            var data = new
            {
                namespace_name = settings.TestProject.TestNameSpace,
                list_of_fixtures = templateData
            };

            string testSources = GenerateTestMethods(data);
            string implementationSources = GenerateTestImplementations(data);

            return new[] { testSources, implementationSources };
        }

        private string GenerateTestMethods(dynamic data)
        {
            Handlebars.RegisterTemplate("TEST_METHOD_TEMPLATE", XUnitTemplate.TEST_METHOD_TEMPLATE);
            Handlebars.RegisterTemplate("ASSERT_STATEMENT_TEMPLATE", XUnitTemplate.ASSERT_STATEMENT_TEMPLATE);
            Handlebars.RegisterTemplate("TEST_DATA_TEMPLATE", XUnitTemplate.TEST_DATA_TEMPLATE);
            Handlebars.RegisterTemplate("TEST_NAME_TEMPLATE", XUnitTemplate.TEST_NAME_TEMPLATE);
            Handlebars.RegisterTemplate("CLASS_VARIABLE_TEMPLATE", XUnitTemplate.CLASS_VARIABLE_TEMPLATE);
            Handlebars.RegisterTemplate("IMPL_METHOD_CALL_EXPRESSION_TEMPLATE", XUnitTemplate.IMPL_METHOD_CALL_EXPRESSION_TEMPLATE);
            Handlebars.RegisterTemplate("IMPL_METHOD_DECLARATION_TEMPLATE", XUnitTemplate.IMPL_METHOD_DECLARATION_TEMPLATE);
            Handlebars.RegisterTemplate("DECORATION_METHOD_TEMPLATE", XUnitTemplate.DECORATION_METHOD_TEMPLATE);
            Handlebars.RegisterTemplate("CLASS_VARIABLE_TEMPLATE", XUnitTemplate.CLASS_VARIABLE_TEMPLATE);
            Handlebars.RegisterTemplate("DECORATION_VARIABLE_TEMPLATE", XUnitTemplate.DECORATION_VARIABLE_TEMPLATE);

            Func<object, string> compiled = Handlebars.Compile(XUnitTemplate.TEST_TEMPLATE);

            return compiled(data);
        }

        private string GenerateTestImplementations(object data)
        {
            Handlebars.RegisterTemplate("TEST_NAME_TEMPLATE", XUnitTemplate.TEST_NAME_TEMPLATE);
            Handlebars.RegisterTemplate("IMPL_METHOD_TEMPLATE", XUnitTemplate.IMPL_METHOD_TEMPLATE);

            Func<object, string> compiled = Handlebars.Compile(XUnitTemplate.IMPLEMENTATION_TEMPLATE);

            return compiled(data);
        }
    }
}
