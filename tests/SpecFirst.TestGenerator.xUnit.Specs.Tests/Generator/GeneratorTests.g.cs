﻿
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by SpecFirst source generator.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SpecFirst.TestGenerator.xUnit.Specs.Tests
{
    using System;
    using System.Collections.Generic;
    using Xunit;
    
    public partial class generate_test_class_name
    {
        
        [Theory]
        [MemberData(nameof(get_test_data))]
        public void generate_test_class_name_tests(string decision_table_name, string test_class_name)
        {
            string test_class_name_output = generate_test_class_name_implementation(decision_table_name);
            Assert.Equal(test_class_name_output, test_class_name);
        }

        public static IEnumerable<object[]> get_test_data()
        {
            var data = new List<object[]>
            {
                new object[] { "decision table name", "public partial class decision_table_name" }, // use snake case for test class name
                new object[] { "Decision Table Name", "public partial class decision_table_name" }, // ingore cases
            };

            return data;
        }

        private partial string generate_test_class_name_implementation(string decision_table_name);
    }

    public partial class generate_class_variables
    {
        
        [Theory]
        [MemberData(nameof(get_test_data))]
        public void generate_class_variables_tests(string decision_table_name, string test_class_name)
        {
            string test_class_name_output = generate_class_variables_implementation(decision_table_name);
            Assert.Equal(test_class_name_output, test_class_name);
        }

        public static IEnumerable<object[]> get_test_data()
        {
            var data = new List<object[]>
            {
                new object[] { "decision table name", "public partial class decision_table_name" }, // use snake case for test class name
                new object[] { "Decision Table Name", "public partial class decision_table_name" }, // ingore cases
            };

            return data;
        }

        private partial string generate_class_variables_implementation(string decision_table_name);
    }

    public partial class generate_test_method
    {
        
        [Theory]
        [MemberData(nameof(get_test_data))]
        public void generate_test_method_tests(string decision_table_name, String[] decision_table_headers, String[] decision_table_data_types, string test_method)
        {
            string test_method_output = generate_test_method_implementation(decision_table_name, decision_table_headers, decision_table_data_types);
            Assert.Equal(test_method_output, test_method);
        }

        public static IEnumerable<object[]> get_test_data()
        {
            var data = new List<object[]>
            {
                new object[] { "decision table name", new string[] {"#Comment", "Header 1", "Header 2", "Header 3?", "Header 4?"}, new string[] {"string", "int", "string", "decimal", "object"}, "public void decision_table_name_tests(int header_1, string header_2, decimal header_3, object header_4)" }, // It ingores comment columns
                new object[] { "Decision Table Name", new string[] {"#Comment", "Header 3?", "Header 4?"}, new string[] {"string", "decimal", "object"}, "public void decision_table_name_tests(decimal header_3, object header_4)" }, // it is ok not to have input columns
                new object[] { "Decision Table Name", new string[] {"#Comment", "Header 1", "Header 2"}, new string[] {"string", "int", "string"}, "public void decision_table_name_tests(int header_1, string header_2)" }, // it is ok not to have output columns
            };

            return data;
        }

        private partial string generate_test_method_implementation(string decision_table_name, String[] decision_table_headers, String[] decision_table_data_types);
    }

    public partial class generate_implementation_method
    {
        
        [Theory]
        [MemberData(nameof(get_test_data))]
        public void generate_implementation_method_tests(string decision_table_name, String[] decision_table_headers, String[] decision_table_data_types, string implementation_method)
        {
            string implementation_method_output = generate_implementation_method_implementation(decision_table_name, decision_table_headers, decision_table_data_types);
            Assert.Equal(implementation_method_output, implementation_method);
        }

        public static IEnumerable<object[]> get_test_data()
        {
            var data = new List<object[]>
            {
                new object[] { "decision table name", new string[] {"#Comment", "Header 1", "Header 2", "Header 3?", "Header 4?"}, new string[] {"string", "int", "string", "decimal", "object"}, "private partial (decimal, object) decision_table_name_implementation(int header_1, string header_2);" }, // It ingores comment columns
                new object[] { "Decision Table Name", new string[] {"#Comment", "Header 3?", "Header 4?"}, new string[] {"string", "decimal", "object"}, "private partial (decimal, object) decision_table_name_implementation();" }, // it is ok not to have input columns
                new object[] { "Decision Table Name", new string[] {"#Comment", "Header 1", "Header 2"}, new string[] {"string", "int", "string"}, "private partial void decision_table_name_implementation(int header_1, string header_2);" }, // it is ok not to have output columns
            };

            return data;
        }

        private partial string generate_implementation_method_implementation(string decision_table_name, String[] decision_table_headers, String[] decision_table_data_types);
    }

    public partial class generate_the_expression_to_call_the_implementation_method
    {
        
        [Theory]
        [MemberData(nameof(get_test_data))]
        public void generate_the_expression_to_call_the_implementation_method_tests(string decision_table_name, String[] decision_table_headers, String[] decision_table_data_types, string expression_for_calling_implementation_method)
        {
            string expression_for_calling_implementation_method_output = generate_the_expression_to_call_the_implementation_method_implementation(decision_table_name, decision_table_headers, decision_table_data_types);
            Assert.Equal(expression_for_calling_implementation_method_output, expression_for_calling_implementation_method);
        }

        public static IEnumerable<object[]> get_test_data()
        {
            var data = new List<object[]>
            {
                new object[] { "decision table name", new string[] {"#Comment", "Header 1", "Header 2", "Header 3?", "Header 4?"}, new string[] {"string", "int", "string", "decimal", "object"}, "(decimal header_3_output, object header_4_output) = decision_table_name_implementation(header_1, header_2);" }, // It ingores comment columns
                new object[] { "Decision Table Name", new string[] {"#Comment", "Header 3?", "Header 4?"}, new string[] {"string", "decimal", "object"}, "(decimal header_3_output, object header_4_output) = decision_table_name_implementation();" }, // it is ok not to have input columns
                new object[] { "Decision Table Name", new string[] {"#Comment", "Header 1", "Header 2"}, new string[] {"string", "int", "string"}, "decision_table_name_implementation(header_1, header_2);" }, // it is ok not to have output columns
            };

            return data;
        }

        private partial string generate_the_expression_to_call_the_implementation_method_implementation(string decision_table_name, String[] decision_table_headers, String[] decision_table_data_types);
    }

    public partial class generate_assert_statement
    {
        
        [Theory]
        [MemberData(nameof(get_test_data))]
        public void generate_assert_statement_tests(string decision_table_name, String[] decision_table_headers, String[] decision_table_data_types, string assert_statement)
        {
            string assert_statement_output = generate_assert_statement_implementation(decision_table_name, decision_table_headers, decision_table_data_types);
            Assert.Equal(assert_statement_output, assert_statement);
        }

        public static IEnumerable<object[]> get_test_data()
        {
            var data = new List<object[]>
            {
                new object[] { "decision table name", new string[] {"#Comment", "Header 1", "Header 2", "Header 3?", "Header 4?"}, new string[] {"string", "int", "string", "decimal", "object"}, "Assert.Equal(header_3_output, header_3); Assert.Equal(header_4_output, header_4);" }, // It ingores comment columns
                new object[] { "Decision Table Name", new string[] {"#Comment", "Header 3?", "Header 4?"}, new string[] {"string", "decimal", "object"}, "Assert.Equal(header_3_output, header_3); Assert.Equal(header_4_output, header_4);" }, // it is ok not to have input columns
                new object[] { "Decision Table Name", new string[] {"#Comment", "Header 1", "Header 2"}, new string[] {"string", "int", "string"}, "" }, // it is ok not to have output columns
            };

            return data;
        }

        private partial string generate_assert_statement_implementation(string decision_table_name, String[] decision_table_headers, String[] decision_table_data_types);
    }

    public partial class generate_test_data
    {
        private static readonly string decision_table = "<table>\n  <tbody>\n    <tr>\n      <td colspan=\"3\"> Table Name </td>\n    </tr>\n    <tr>\n      <td> #Description </td>\n      <td> Integer </td>\n      <td> Decimal </td>\n      <td> Double </td>\n      <td> Boolean? </td>\n      <td> String? </td>\n      <td> DateTime? </td>\n    </tr>\n    <tr>\n      <td> Row 1 </td>\n      <td> 12 </td>\n      <td> 12.5M </td>\n      <td> 12.5 </td>\n      <td> True </td>\n      <td> \"text\" </td>\n      <td> 2012-03-26 12:12:12 </td>\n    </tr>\n    <tr>\n      <td> Row 2 </td>\n      <td> 12 </td>\n      <td> 12.5M </td>\n      <td> 12.5D </td>\n      <td> False </td>\n      <td> \"text\" </td>\n      <td> 2012-03-26 12:12:12 </td>\n    </tr>\n    <tr>\n      <td> Row 3 </td>\n      <td> 12 </td>\n      <td> 12M </td>\n      <td> 12D </td>\n      <td> false </td>\n      <td> \"text\" </td>\n      <td> 2012-03-26 12:12:12 </td>\n    </tr>\n  </tbody>\n</table>\n";
        private static readonly string test_data = "public static IEnumerable<object[]> get_test_data()\n{\n    var data = new List<object[]>\n    {\n        new object[] { 12, 12.5M, 12.5D, true, \"text\", new DateTime(2012, 3, 26, 12, 12, 12, 0) }, // Row 1\n        new object[] { 12, 12.5M, 12.5D, false, \"text\", new DateTime(2012, 3, 26, 12, 12, 12, 0) }, // Row 2\n        new object[] { 12, 12M, 12D, false, \"text\", new DateTime(2012, 3, 26, 12, 12, 12, 0) }, // Row 3\n    };\n<br/>\n    return data;\n}";
        
        [Theory]
        [MemberData(nameof(get_test_data))]
        public void generate_test_data_tests(string decision_table, string test_data)
        {
            string test_data_output = generate_test_data_implementation(decision_table);
            Assert.Equal(test_data_output, test_data);
        }

        public static IEnumerable<object[]> get_test_data()
        {
            var data = new List<object[]>
            {
                new object[] { decision_table, test_data }, // Generate test data
            };

            return data;
        }

        private partial string generate_test_data_implementation(string decision_table);
    }

}