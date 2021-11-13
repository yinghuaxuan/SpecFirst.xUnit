﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by SpecFirst source generator.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SpecFirst.Core.Specs.Tests.DecisionTable.Validator.DecisionVariableValidator
{
    using System;
    using System.Collections.Generic;
    using Xunit;
    
    public partial class validate_decision_variables_from_links
    {
        [Theory]
        [MemberData(nameof(get_test_data))]
        public void validate_decision_variables_from_links_tests(string text, bool contain_variable, string variable_name, string variable_type, string variable_value)
        {

            (bool contain_variable_output, string variable_name_output, string variable_type_output, string variable_value_output) = validate_decision_variables_from_links_implementation(text);
            Assert.Equal(contain_variable_output, contain_variable);
            Assert.Equal(variable_name_output, variable_name);
            Assert.Equal(variable_type_output, variable_type);
            Assert.Equal(variable_value_output, variable_value);
        }

        public static IEnumerable<object[]> get_test_data()
        {
            var data = new List<object[]>
            {
                new object[] { "<a href=\"\" title=\"$variable_name\" data-href=\"\">this is a variable</a>", true, "variable_name", "string", "this is a variable" }, // variable must start with $ symbol followed by letter
                new object[] { "<a href=\"\" title=\"$variable123\" data-href=\"\">this is a variable</a>", true, "variable123", "string", "this is a variable" }, // variable must start with $ symbol followed by letter
                new object[] { "<a href=\"\" title=\"$123\" data-href=\"\">this is a variable</a>", false, null, null, null }, // variable start with digit is not valid
                new object[] { "<a href=\"\" title=\"variable_name\" data-href=\"\">this is a variable</a>", false, null, null, null }, // variable not start with $ symbol is not valid
                new object[] { "<a href=\"\" title=\"variable_$name\" data-href=\"\">this is a variable</a>", false, null, null, null }, // variable with $ symbol not at the start is not valid
            };

            return data;
        }

        private partial (bool, string, string, string) validate_decision_variables_from_links_implementation(string text);

    }

}