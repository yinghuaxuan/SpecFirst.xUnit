namespace SpecFirst.TestGenerator.xUnit.Specs.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Linq;
    using Core.Serialization;
    using SpecFirst.Core.DecisionTable;
    using SpecFirst.Core.DecisionTable.Parser;
    using SpecFirst.Core.DecisionVariable;
    using SpecFirst.Core.NamingStrategy;
    using SpecFirst.TestGenerator.xUnit.Generator;

    public partial class generate_test_class_name
    {
        private partial string generate_test_class_name_implementation(string decision_table_name)
        {
            var generator =
                new TestClassDeclarationGenerator(new TableNameToClassNameConverter(new SnakeCaseNamingStrategy()));
            return generator.Convert(decision_table_name).TrimStart('\r', '\n');
        }
    }

    public partial class generate_class_fields
    {
        private partial string generate_class_fields_implementation(string decision_variable_name, string decision_variable_value)
        {
            var primitiveDataSerializer = new PrimitiveDataSerializer();
            var generator = new ClassFieldGenerator(primitiveDataSerializer);
            var decisionVariable =
                new DecisionVariable(decision_variable_name, string.IsNullOrEmpty(decision_variable_value) ? typeof(object) : typeof(string), decision_variable_value);
            return generator.Convert(new[] {decisionVariable}).TrimStart('\r', '\n').TrimEnd('\r', '\n');
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

            return generator.Convert(decision_table_name, headers.ToArray()).TrimStart('\r', '\n');
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

            return generator.Convert(decision_table_name, headers.ToArray()).TrimStart('\r', '\n');

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

            return generator.Convert(decision_table_name, headers.ToArray()).TrimStart('\r', '\n').TrimEnd('\r', '\n');
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

            var generator = new AssertStatementGenerator(
                new TableHeaderToParameterConverter(new SnakeCaseNamingStrategy()));

            return generator.Convert(headers.ToArray()).TrimEnd('\r', '\n').Replace("\r\n", " ");
        }
    }

    public partial class generate_test_data
    {
        private partial string generate_test_data_implementation(string decision_table)
        {
            DecisionTableParser parser = new DecisionTableParser();
            var decisionTable = parser.Parse(XElement.Parse(decision_table), new List<DecisionVariable>());

            var primitiveDataSerializer = new PrimitiveDataSerializer();
            var arrayDataSerializer = new ArrayDataSerializer(primitiveDataSerializer);
            var variableSerializer = new TableVariableSerializer();
            var generator = new TestDataGenerator(primitiveDataSerializer, arrayDataSerializer, variableSerializer);

            var data = generator.Convert(decisionTable.TableHeaders, decisionTable.TableData).TrimStart('\r', '\n').TrimEnd('\r', '\n').Replace("\r", "");
            return data;
        }

        //private partial string pre_process_test_data(string test_data, StringProcessingOptions options)
        //{
        //    return test_data.Normalize(options);
        //}
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