﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by SpecFirst source generator.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SpecFirst.TestGenerator.xUnit.Specs.Tests.XUnitTestsGenerator
{
    using System;
    using System.Collections.Generic;
    using Xunit;
    
    public partial class generate_xunit_tests
    {
        private static readonly string decision_table = "<table>   <tbody>     <tr>       <td colspan=\"3\"> Parse a decision table </td>     </tr>     <tr>       <td> #Description </td>       <td> Integer Value </td>       <td> Decimal Value </td>       <td> Double Value </td>       <td> Boolean Value? </td>       <td> <a href=\"#\" title=\"ignore_case|ignore_all_spaces|ignore_line_ending\" data-href=\"#\">String Value</a>? </td>       <td> DateTime Value? </td>     </tr>     <tr>       <td> Row 1 </td>       <td> 12 </td>       <td> 12.5M </td>       <td> 12.5 </td>       <td> True </td>       <td> \"text\" </td>       <td> 2012-03-26 12:12:12 </td>     </tr>     <tr>       <td> Row 2 </td>       <td> 12 </td>       <td> 12.5M </td>       <td> 12.5D </td>       <td> False </td>       <td> \"text\" </td>       <td> 2012-03-26 12:12:12 </td>     </tr>     <tr>       <td> Row 3 </td>       <td> 12 </td>       <td> 12M </td>       <td> 12D </td>       <td> false </td>       <td> \"text\" </td>       <td> 2012-03-26 12:12:12 </td>     </tr>   </tbody> </table> ";
        private static readonly string xunit_test = " //------------------------------------------------------------------------------ // <auto-generated> //     This code was generated by SpecFirst source generator. // //     Manual changes to this file may cause unexpected behavior in your application. //     Manual changes to this file will be overwritten if the code is regenerated. // </auto-generated> //------------------------------------------------------------------------------ <br/> namespace TestProject {   using System;   using System.Collections.Generic;   using Xunit; <br/>   public partial class parse_a_decision_table   {     [Theory]     [MemberData(nameof(get_test_data))]     public void parse_a_decision_table_tests(int integer_value, decimal decimal_value, double double_value, bool boolean_value, string string_value, DateTime datetime_value)     {       string string_value_decoration = \"ignore_case|ignore_all_spaces|ignore_line_ending\";       (bool boolean_value_output, string string_value_output, DateTime datetime_value_output) = parse_a_decision_table_implementation(integer_value, decimal_value, double_value);       Assert.Equal(boolean_value_output, boolean_value);       Assert.Equal(string_value_decoration_implementation(string_value_output, string_value_decoration), string_value_decoration_implementation(string_value, string_value_decoration));       Assert.Equal(datetime_value_output, datetime_value);     }   <br/>     public static IEnumerable<object[]> get_test_data()     {       var data = new List<object[]>       {         new object[] { 12, 12.5M, 12.5D, true, \"text\", new DateTime(2012, 3, 26, 12, 12, 12, 0) }, // Row 1         new object[] { 12, 12.5M, 12.5D, false, \"text\", new DateTime(2012, 3, 26, 12, 12, 12, 0) }, // Row 2         new object[] { 12, 12M, 12D, false, \"text\", new DateTime(2012, 3, 26, 12, 12, 12, 0) }, // Row 3       };   <br/>       return data;     }   <br/>     private partial (bool, string, DateTime) parse_a_decision_table_implementation(int integer_value, decimal decimal_value, double double_value);   <br/>     private partial string string_value_decoration_implementation(string string_value, string string_value_decoration);   } }";


        [Theory]
        [MemberData(nameof(get_test_data))]
        public void generate_xunit_tests_tests(string decision_table, string test_data)
        {
            string test_data_decoration = "ignore_case|ignore_all_spaces";

            string test_data_output = generate_xunit_tests_implementation(decision_table);
            Assert.Equal(test_data_decoration_implementation(test_data_output, test_data_decoration), test_data_decoration_implementation(test_data, test_data_decoration));
        }

        public static IEnumerable<object[]> get_test_data()
        {
            var data = new List<object[]>
            {
                new object[] { decision_table, xunit_test }, // Generate test data
            };

            return data;
        }

        private partial string generate_xunit_tests_implementation(string decision_table);

        private partial string test_data_decoration_implementation(string test_data, string test_data_decoration);
    }

}