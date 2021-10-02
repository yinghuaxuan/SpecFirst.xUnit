namespace SpecFirst.TestGenerator.xUnit.Specs.Tests
{
    using System;
    using System.Linq;
    using System.Xml.Linq;
    using Core.DecisionTable.Parser;
    using Core.DecisionVariable;
    using Core.Setting;

    public partial class generate_xunit_tests
    {
        private partial string generate_xunit_tests_implementation(string decision_table)
        {
            var decisionTable = new DecisionTableParser().Parse(XElement.Parse(decision_table), Enumerable.Empty<DecisionVariable>());
            var generator = new XUnitTestsGenerator();
            var sources = generator.Generate(new SpecFirstSettings{TestGeneration = new TestGeneration{TestProject = "TestProject"}}, new[] { decisionTable });
            return sources.ElementAt(0);
        }

        private partial string test_data_decoration(string test_data, string test_data_decoration)
        {
            return test_data.Normalize();
        }
    }
}