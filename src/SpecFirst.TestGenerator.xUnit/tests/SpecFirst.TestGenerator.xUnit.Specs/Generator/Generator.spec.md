The converters are responsible for converting the DecisionTable object into unit tests in the target framework. For this testing, xUnit is our target framework.  

An example of the converted unit test:
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

### TestClassNameGenerator
The `TestClassNameGenerator` is responsible for converting the table name into the test class name. Each decision table will be converted to a separate test class. The generator takes the table name string literal, convert it into a snake case (all in lower case) and generate the test class name.

| Generate test class name                                                                          |||
| #Comment                           | Decision Table Name | Test Class Name?                         |
| ---------------------------------- | ------------------- | ---------------------------------------- |
| use snake case for test class name | decision table name | public partial class decision_table_name |
| ingore cases                       | Decision Table Name | public partial class decision_table_name |

TODO: should remove any illegal characters

### ClassFieldsGenerator
The `ClassFieldsGenerator` is responsible for generating the fields from decision variables. 

| Generate class fields                                                                                                                   ||||
| #Comment                    | Decision Variable Name | Decision Variable Value | Field?                                                    |
| --------------------------- | ---------------------- | ----------------------- | --------------------------------------------------------- |
| variable has a value        | variable_1             | variable 1              | private static readonly string variable_1 = "variable 1"; |
| variable don't have a value | variable_2             | null                    | private static readonly object variable_2;                |

### TestMethodGenerator
The `TestMethodGenerator` is responsible for generating the test method with the method name, method parameters.  
The method name is the test class name suffixed by `_tests`
The return value will always be `void`; in other words, there is no return values. 

| Generate test method                                                                                                                                                                                                                                                             |||||
| #Description                        | Decision Table Name | Decision Table Headers                                     | Decision Table Data Types                        | Test Method?                                                                                             |
| ----------------------------------- | ------------------- | ---------------------------------------------------------- | ------------------------------------------------ | -------------------------------------------------------------------------------------------------------- |
| It ingores comment columns          | decision table name | ["#Comment","Header 1","Header 2","Header 3?","Header 4?"] | ["string", "int", "string", "decimal", "object"] | public void decision_table_name_tests(int header_1, string header_2, decimal header_3, object header_4) |
| it is ok not to have input columns  | Decision Table Name | ["#Comment","Header 3?","Header 4?"]                       | ["string", "decimal", "object"]                  | public void decision_table_name_tests(decimal header_3, object header_4)                                |
| it is ok not to have output columns | Decision Table Name | ["#Comment","Header 1","Header 2"]                         | ["string", "int", "string"]                      | public void decision_table_name_tests(int header_1, string header_2)                                    |

### ImplMethodGenerator
The `TestMethodGenerator` is responsible for generating the implementation method with the method name, method parameters and return types.  
The method will be implemented as private and partial.
The method name is the test class name suffixed by `_impl`
The return value can be:
- void if the table doesn't contain any output columns
- the type of the column if there is only one output column
- a tuple of types if there are multiple output columns

| Generate implementation method                                                                                                                                                                                                                                              |||||
| #Description                        | Decision Table Name | Decision Table Headers                                     | Decision Table Data Types                        | Implementation Method?                                                                              |
| ----------------------------------- | ------------------- | ---------------------------------------------------------- | ------------------------------------------------ | --------------------------------------------------------------------------------------------------- |
| It ingores comment columns          | decision table name | ["#Comment","Header 1","Header 2","Header 3?","Header 4?"] | ["string", "int", "string", "decimal", "object"] | private partial (decimal, object) decision_table_name_implementation(int header_1, string header_2) |
| it is ok not to have input columns  | Decision Table Name | ["#Comment","Header 3?","Header 4?"]                       | ["string", "decimal", "object"]                  | private partial (decimal, object) decision_table_name_implementation()                              |
| it is ok not to have output columns | Decision Table Name | ["#Comment","Header 1","Header 2"]                         | ["string", "int", "string"]                      | private partial void decision_table_name_implementation(int header_1, string header_2)              |

### ImplMethodCallExpressionGenerator
The `ImplMethodCallExpressionGenerator` is responsible for generating the expression to call the implementation method.  

| Generate the expression to call the implementation method                                                                                                                                                                                                                                                 |||||
| #Description                                              | Decision Table Name | Decision Table Headers                                     | Decision Table Data Types                        | Expression for calling implementation method?                                                               |
| --------------------------------------------------------- | ------------------- | ---------------------------------------------------------- | ------------------------------------------------ | ----------------------------------------------------------------------------------------------------------- |
| It ingores comment columns                                | decision table name | ["#Comment","Header 1","Header 2","Header 3?","Header 4?"] | ["string", "int", "string", "decimal", "object"] | (decimal header_3_output, object header_4_output) = decision_table_name_implementation(header_1, header_2); |
| it is ok not to have input columns                        | Decision Table Name | ["#Comment","Header 3?","Header 4?"]                       | ["string", "decimal", "object"]                  | (decimal header_3_output, object header_4_output) = decision_table_name_implementation();                   |
| it is ok not to have output columns                       | Decision Table Name | ["#Comment","Header 1","Header 2"]                         | ["string", "int", "string"]                      | decision_table_name_implementation(header_1, header_2);                                                     |

### AssertStatementGenerator
The `AssertStatementGenerator` is responsible for generating the assert statement to compare the expected and actual values.  

| Generate assert statement                                                                                                                                                                                                         |||||
| #Description                        | Decision Table Name | Decision Table Headers                                     | Decision Table Data Types                        | [Assert Statement](# "ignore_case\|ignore_all_spaces")? |
| ----------------------------------- | ------------------- | ---------------------------------------------------------- | ------------------------------------------------ | --------------------------------------------------------- |
| It ingores comment columns          | decision table name | ["#Comment","Header 1","Header 2","Header 3?","Header 4?"] | ["string", "int", "string", "decimal", "object"] | Assert.Equal(header_3_output, header_3);                  |\
|                                     |                     |                                                            |                                                  | Assert.Equal(header_4_output, header_4);                  |
| it is ok not to have input columns  | Decision Table Name | ["#Comment","Header 3?","Header 4?"]                       | ["string", "decimal", "object"]                  | Assert.Equal(header_3_output, header_3);                  |\
|                                     |                     |                                                            |                                                  | Assert.Equal(header_4_output, header_4);                  |
| it is ok not to have output columns | Decision Table Name | ["#Comment","Header 1","Header 2"]                         | ["string", "int", "string"]                      |                                                           |

### TestDataGenerator
The `TestDataGenerator` is responsible for generating the test data for the testing. It generates a method `get_test_data` and this method is used as an attribute `[MemberData(nameof(get_test_data))]` on the testing method.

with a decision table like below:  
[\<table>  
&nbsp;&nbsp;\<tbody>  
&nbsp;&nbsp;&nbsp;&nbsp;\<tr>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td colspan="3"> Table Name \</td>    
&nbsp;&nbsp;&nbsp;&nbsp;\</tr>  
&nbsp;&nbsp;&nbsp;&nbsp;\<tr>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> #Description \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> Integer \</td>    
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> Decimal \</td>   
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> Double \</td>   
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> Boolean? \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> String? \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> DateTime? \</td>   
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
[public static IEnumerable\<object[]> get_test_data()  
{  
&nbsp;&nbsp;&nbsp;&nbsp;var data = new List\<object[]>  
&nbsp;&nbsp;&nbsp;&nbsp;{  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;new object[] { 12, 12.5M, 12.5D, true, "text", new DateTime(2012, 3, 26, 12, 12, 12, 0) }, // Row 1  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;new object[] { 12, 12.5M, 12.5D, false, "text", new DateTime(2012, 3, 26, 12, 12, 12, 0) }, // Row 2  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;new object[] { 12, 12M, 12D, false, "text", new DateTime(2012, 3, 26, 12, 12, 12, 0) }, // Row 3  
&nbsp;&nbsp;&nbsp;&nbsp;};  
<br/>
&nbsp;&nbsp;&nbsp;&nbsp;return data;  
}](# "$test_data")

| comment:Generate test data                                                                    |||
| #Comment                   | Decision Table  | [Test Data](# "ignore_case\|ignore_all_spaces")? |
| -------------------------- | --------------- | ------------------------------------------------ |
| Generate test data         | $decision_table | $test_data                                       |