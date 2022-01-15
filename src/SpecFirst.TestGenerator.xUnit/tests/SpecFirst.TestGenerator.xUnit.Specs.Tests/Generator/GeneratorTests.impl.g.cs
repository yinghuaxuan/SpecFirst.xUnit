namespace SpecFirst.TestGenerator.xUnit.Specs.Tests.Generator.Generator
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Linq;
    using SpecFirst.Core.Serialization;
    using SpecFirst.Core.Utils;
    using HandlebarsDotNet;
    using SpecFirst.Core.DecisionTable;
    using SpecFirst.Core.DecisionTable.Parser;
    using SpecFirst.Core.DecisionVariable;
    using SpecFirst.Core.NamingStrategy;
    using SpecFirst.TestGenerator.xUnit.Generator;
    using SpecFirst.TestGenerator.xUnit.Template;

    public partial class generate_test_class_name
    {
        private partial string generate_test_class_name_implementation(string decision_table_name)
        {
            var generator =
                new TestClassDeclarationGenerator(new TableNameToClassNameConverter(new SnakeCaseNamingStrategy()));

            var template = Handlebars.Compile(XUnitTemplate.TEST_NAME_TEMPLATE);
            var data = generator.Convert(new DecisionTable(TableType.Decision, decision_table_name, null, null));

            return template(data).Trim();
        }
    }

    public partial class generate_class_fields
    {
        private partial string generate_class_fields_implementation(string decision_variable_name, string decision_variable_value)
        {
            var primitiveDataSerializer = new SingularDataSerializer();
            var generator = new ClassFieldsGenerator(primitiveDataSerializer);
            var decisionVariable =
                new DecisionVariable(decision_variable_name, string.IsNullOrEmpty(decision_variable_value) ? typeof(object) : typeof(string), decision_variable_value);

            var template = Handlebars.Compile(XUnitTemplate.CLASS_VARIABLE_TEMPLATE);
            var data = generator.Convert(new DecisionTable(TableType.Decision, "decision_table_name", null, null, new[] { decisionVariable }));

            return template(data).Trim();
        }
    }

    public partial class generate_test_method : generator_base
    {
        private partial string generate_test_method_implementation(
            string decision_table_name,
            String[] decision_table_headers,
            String[] decision_table_data_types)
        {
            var headers = ParseTableHeaderFromString(decision_table_headers, decision_table_data_types);

            var generator = new TestMethodGenerator(
                new TableHeaderToParameterConverter(new SnakeCaseNamingStrategy()),
                new TableNameToClassNameConverter(new SnakeCaseNamingStrategy()));

            var template = Handlebars.Compile(XUnitTemplate.TEST_METHOD_TEMPLATE);
            var data = generator.Convert(new DecisionTable(TableType.Decision, decision_table_name, headers.ToArray(), null));

            return template(data).Trim();
        }
    }

    public partial class generate_implementation_method : generator_base
    {
        private partial string generate_implementation_method_implementation(
            string decision_table_name,
            String[] decision_table_headers, String[] decision_table_data_types)
        {
            var headers = ParseTableHeaderFromString(decision_table_headers, decision_table_data_types);

            var generator = new ImplMethodDeclarationGenerator(
                new TableHeaderToParameterConverter(new SnakeCaseNamingStrategy()),
                new TableNameToClassNameConverter(new SnakeCaseNamingStrategy()));

            var template = Handlebars.Compile(XUnitTemplate.IMPL_METHOD_TEMPLATE);
            var data = generator.Convert(new DecisionTable(TableType.Decision, decision_table_name, headers.ToArray(), null));

            return template(data).Trim();

        }
    }

    public partial class generate_the_expression_to_call_the_implementation_method : generator_base
    {
        private partial string generate_the_expression_to_call_the_implementation_method_implementation(
            string decision_table_name,
            String[] decision_table_headers,
            String[] decision_table_data_types)
        {
            var headers = ParseTableHeaderFromString(decision_table_headers, decision_table_data_types);

            var generator = new ImplMethodCallExpressionGenerator(
                new TableHeaderToParameterConverter(new SnakeCaseNamingStrategy()),
                new TableNameToClassNameConverter(new SnakeCaseNamingStrategy()));

            var template = Handlebars.Compile(XUnitTemplate.IMPL_METHOD_CALL_EXPRESSION_TEMPLATE);
            var data = generator.Convert(new DecisionTable(TableType.Decision, decision_table_name, headers.ToArray(), null));

            return template(data).Trim();
        }
    }

    public partial class generate_assert_statement : generator_base
    {
        private partial string generate_assert_statement_implementation(
            string decision_table_name,
            String[] decision_table_headers,
            String[] decision_table_data_types)
        {
            var headers = ParseTableHeaderFromString(decision_table_headers, decision_table_data_types);

            var generator = new AssertStatementsGenerator(
                new TableHeaderToParameterConverter(new SnakeCaseNamingStrategy()));

            var template = Handlebars.Compile(XUnitTemplate.ASSERT_STATEMENT_TEMPLATE);
            var data = generator.Convert(new DecisionTable(TableType.Decision, "table name", headers.ToArray(), null));

            return template(data).Trim();
        }

        private partial string assert_statement_decoration_implementation(
            string assert_statement,
            string assert_statement_decoration)
        {
            var options = default(StringProcessingOptions);
            if (assert_statement_decoration.Contains("ignore_all_spaces"))
            {
                options = options | StringProcessingOptions.IgnoreAllSpaces;
            }
            if (assert_statement_decoration.Contains("ignore_case"))
            {
                options = options | StringProcessingOptions.IgnoreCase;
            }
            assert_statement = assert_statement?.Normalize(options);
            return assert_statement ?? string.Empty;
        }
    }

    public partial class generate_test_data
    {
        private partial string generate_test_data_implementation(string decision_table)
        {
            DecisionTableParser parser = new DecisionTableParser();
            var decisionTable = parser.Parse(XElement.Parse(decision_table), new List<DecisionVariable>());

            var singularDataSerializer = new SingularDataSerializer();
            var arrayDataSerializer = new ArrayDataSerializer(singularDataSerializer);
            var generator = new TestDataGenerator(singularDataSerializer, arrayDataSerializer);

            var template = Handlebars.Compile(XUnitTemplate.TEST_DATA_TEMPLATE);
            var data = generator.Convert(decisionTable);
            
            return template(data).TrimStart('\r', '\n').TrimEnd('\r', '\n').Replace("\r", "");
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

    public class generator_base
    {
        protected static List<TableHeader> ParseTableHeaderFromString(
            string[] decision_table_headers,
            string[] decision_table_data_types)
        {
            var headers = new List<TableHeader>();
            var tableHeaderParser = new TableHeaderParser();
            for (int i = 0; i < decision_table_headers.Length; i++)
            {
                var header = tableHeaderParser.Parse(XElement.Parse($"<th>{decision_table_headers[i]}</th>"));
                header.UpdateDataType(TypeHelper.GetTypeFromString(decision_table_data_types[i]));
                headers.Add(header);
            }

            return headers;
        }
    }
}