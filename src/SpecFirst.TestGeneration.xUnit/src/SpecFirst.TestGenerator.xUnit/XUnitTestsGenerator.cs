﻿namespace SpecFirst.TestGenerator.xUnit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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

        public IEnumerable<string> Generate(SpecFirstSettings settings, IEnumerable<DecisionTable> decisionTables)
        {
            XUnitTemplateData[] templateData = _templateDataProvider.GetTemplateData(decisionTables);
            var data = new
            {
                namespace_name = settings.TestGeneration.TestProject,
                list_of_fixtures = templateData.Select(t => new
                {
                    class_name = t.ClassName,
                    test_parameters = t.TestMethodParameters,
                    impl_arguments = t.ImplMethodArguments,
                    impl_parameters = t.ImplMethodParameters,
                    impl_return_values = t.ImplMethodReturnValues,
                    impl_return_types = t.ImplMethodReturnTypes,
                    assert_statements = t.AssertStatements,
                    test_data_and_comments = t.TestDataAndComments
                })
            };

            string testSources = GenerateTestMethods(data);
            string implementationSources = GenerateTestImplementations(data);

            return new[] { testSources, implementationSources };
        }

        private string GenerateTestMethods(object data)
        {
            Func<object, string> compiled = Handlebars.Compile(XUnitTemplate.TEST_TEMPLATE);

            return compiled(data);
        }

        private string GenerateTestImplementations(object data)
        {
            Func<object, string> compiled = Handlebars.Compile(XUnitTemplate.IMPLEMENTATION_TEMPLATE);

            return compiled(data);
        }
    }
}