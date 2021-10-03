The XUnitTestsGenerator is the top level generator that utilizes the [individual generators](Generator\Generator.spec.md) to generate the complete unit tests in xUnit test framework.  

An example of the generated unit test:
<pre><code>
public partial class parse_a_decision_table
{
    private static readonly string decision_table_default = "some value";
    private static readonly string decision_table = "some value";
    private static readonly string decision_table_with_theader = "some value";
    private static readonly string comment_decision_table = "some value";
    private static readonly string setup_decision_table = "some value";
    
    [Theory]
    [MemberData(nameof(get_test_data))]
    public void parse_a_decision_table_tests(string decision_table, string table_type, string table_name, string input_table_header, string output_table_header, string comment_table_header)
    {
        (string table_type_output, string table_name_output, string input_table_header_output, string output_table_header_output, string comment_table_header_output) = parse_a_decision_table_implementation(decision_table);
        Assert.Equal(table_type_output, table_type);
        Assert.Equal(table_name_output, table_name);
        Assert.Equal(input_table_header_output, input_table_header);
        Assert.Equal(output_table_header_output, output_table_header);
        Assert.Equal(comment_table_header_output, comment_table_header);
    }

    public static IEnumerable< object[]> get_test_data()
    {
        var data = new List< object[]>
        {
            new object[] { decision_table_default, "Decision", "Table Name", "Table Header 1", "Table Header 2", "Description" }, // Decision table without prefix
            new object[] { decision_table, "Decision", "Table Name", "Table Header 1", "Table Header 2", "Description" }, // Decision table
            new object[] { decision_table_with_theader, "Decision", "Table Name", "Table Header 1", "Table Header 2", "Description" }, // Decision table with th headers
            new object[] { comment_decision_table, "Comment", "Table Name", "Table Header 1", "Table Header 2", "Description" }, // Comment decision table
            new object[] { setup_decision_table, "Setup", "Table Name", "Table Header 1", "Table Header 2", "Description" }, // Setup decision table
        };

        return data;
    }

    private partial (string, string, string, string, string) parse_a_decision_table_implementation(string decision_table);
}
</code></pre>

### XUnitTestsGenerator
The `XUnitTestsGenerator` is responsible for generating the complete xUnit tests from the decision table.

with a decision table like below:  
[\<table>  
&nbsp;&nbsp;\<tbody>  
&nbsp;&nbsp;&nbsp;&nbsp;\<tr>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td colspan="3"> Parse a decision table \</td>    
&nbsp;&nbsp;&nbsp;&nbsp;\</tr>  
&nbsp;&nbsp;&nbsp;&nbsp;\<tr>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> #Description \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> Integer Value \</td>    
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> Decimal Value \</td>   
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> Double Value \</td>   
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> Boolean Value? \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> <a href="#" title="ignore_case|ignore_all_spaces|ignore_line_ending" data-href="#">String Value</a>? \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> DateTime Value? \</td>   
&nbsp;&nbsp;&nbsp;&nbsp;\</tr>  
&nbsp;&nbsp;&nbsp;&nbsp;\<tr>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> Row 1 \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> 12 \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> 12.5M \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> 12.5 \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> True \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> "text" \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> 2012-03-26 12:12:12 \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;\</tr>    
&nbsp;&nbsp;&nbsp;&nbsp;\<tr>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> Row 2 \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> 12 \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> 12.5M \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> 12.5D \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> False \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> "text" \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> 2012-03-26 12:12:12 \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;\</tr>   
&nbsp;&nbsp;&nbsp;&nbsp;\<tr>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> Row 3 \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> 12 \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> 12M \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> 12D \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> false \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> "text" \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> 2012-03-26 12:12:12 \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;\</tr>   
&nbsp;&nbsp;\</tbody>  
\</table>
](# "$decision_table")

It should generate the test data like this:  
[
//------------------------------------------------------------------------------  
// \<auto-generated>  
//     This code was generated by SpecFirst source generator.  
//  
//     Manual changes to this file may cause unexpected behavior in your application.  
//     Manual changes to this file will be overwritten if the code is regenerated.  
// \</auto-generated>  
//------------------------------------------------------------------------------  
<br/>
namespace TestProject    
{  
&nbsp;&nbsp;using System;  
&nbsp;&nbsp;using System.Collections.Generic;  
&nbsp;&nbsp;using Xunit;  
<br/>
&nbsp;&nbsp;public partial class parse_a_decision_table  
&nbsp;&nbsp;{  
&nbsp;&nbsp;&nbsp;&nbsp;[Theory]  
&nbsp;&nbsp;&nbsp;&nbsp;[MemberData(nameof(get_test_data))]  
&nbsp;&nbsp;&nbsp;&nbsp;public void parse_a_decision_table_tests(int integer_value, decimal decimal_value, double double_value, bool boolean_value, string string_value, DateTime datetime_value)  
&nbsp;&nbsp;&nbsp;&nbsp;{  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;string string_value_decoration = "ignore_case|ignore_all_spaces|ignore_line_ending";      
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(bool boolean_value_output, string string_value_output, DateTime datetime_value_output) = parse_a_decision_table_implementation(integer_value, decimal_value, double_value);  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Assert.Equal(boolean_value_output, boolean_value);  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Assert.Equal(string_value_decoration_implementation(string_value_output, string_value_decoration), string_value_decoration_implementation(string_value, string_value_decoration));  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Assert.Equal(datetime_value_output, datetime_value);   
&nbsp;&nbsp;&nbsp;&nbsp;}  
&nbsp;&nbsp;<br/>
&nbsp;&nbsp;&nbsp;&nbsp;public static IEnumerable\<object[]> get_test_data()  
&nbsp;&nbsp;&nbsp;&nbsp;{  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var data = new List\<object[]>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;new object[] { 12, 12.5M, 12.5D, true, "text", new DateTime(2012, 3, 26, 12, 12, 12, 0) }, // Row 1  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;new object[] { 12, 12.5M, 12.5D, false, "text", new DateTime(2012, 3, 26, 12, 12, 12, 0) }, // Row 2  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;new object[] { 12, 12M, 12D, false, "text", new DateTime(2012, 3, 26, 12, 12, 12, 0) }, // Row 3  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;};  
&nbsp;&nbsp;<br/>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;return data;  
&nbsp;&nbsp;&nbsp;&nbsp;}  
&nbsp;&nbsp;<br/>
&nbsp;&nbsp;&nbsp;&nbsp;private partial (bool, string, DateTime) parse_a_decision_table_implementation(int integer_value, decimal decimal_value, double double_value);
&nbsp;&nbsp;<br/>
&nbsp;&nbsp;&nbsp;&nbsp;private partial string string_value_decoration_implementation(string string_value, string string_value_decoration);
&nbsp;&nbsp;}
}](# "$xunit_test")

| comment:Generate xUnit Tests                                                                    |||
| #Comment                     | Decision Table  | [Test Data](# "ignore_case\|ignore_all_spaces")? |
| ---------------------------- | --------------- | ------------------------------------------------ |
| Generate test data           | $decision_table | $xunit_test                                      |