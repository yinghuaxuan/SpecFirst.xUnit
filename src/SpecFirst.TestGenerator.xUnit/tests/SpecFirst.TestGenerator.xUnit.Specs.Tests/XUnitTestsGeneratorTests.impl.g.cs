namespace SpecFirst.TestGenerator.xUnit.Specs.Tests.XUnitTestsGenerator
{
    using System;
    using System.Linq;
    using System.Xml.Linq;
    using SpecFirst.Core.DecisionTable.Parser;
    using SpecFirst.Core.DecisionVariable;
    using SpecFirst.Core.Setting;
    using SpecFirst.Core.Utils;
    using SpecFirst.TestGenerator.xUnit;

    public partial class generate_xunit_tests
    {
        private partial string generate_xunit_tests_implementation(string decision_table)
        {
            var decisionTable = new DecisionTableParser().Parse(XElement.Parse(decision_table), Enumerable.Empty<DecisionVariable>());
            var generator = new XUnitTestsGenerator();
            var sources = generator.Generate(new SpecFirstSettings{TestProject = new TestProject{TestNameSpace = "TestProject"}}, new[] { decisionTable });
            return sources.ElementAt(0);
        }

        private partial string test_data_decoration_implementation(string test_data, string test_data_decoration)
        {
            var options = default(StringProcessingOptions);
            if (test_data_decoration.Contains("ignore_all_spaces"))
            {
                options = options | StringProcessingOptions.IgnoreAllSpaces;
            }
            if (test_data_decoration.Contains("ignore_case"))
            {
                options = options | StringProcessingOptions.IgnoreCase;
            }
            test_data = test_data.Normalize(options);
            test_data = test_data.Replace(@"<BR/>", string.Empty);
            return test_data;
        }
    }
}