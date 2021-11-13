The `DecisionTableParser` takes the html output of the markdown parser, extracts all the decision tables, and parse each decision table to a decision table object.  

The `DecisionTableParser` utilizes the [component parsers]("DecisionTableComponentParser.spec.md") to parse each componen in the decision table. But it is more than just to assemble the results from each component parser - it performs the following additional actions:

- Parse Decision Variables
    - Go through each decision variable and replace the varialbe with its real type and value
    - If the decision variable is not already defined, it will ignore the varialbe and interpret it as a string unless the varialbe is located in a Setup table
    - If the decision variable is in a Setup table and is not already defined, it will create the variable with type as `object` and value as `null`
- Update column types
    - Go through each data type in the column and figure out the most compatible type for the column

## Vanilla Decision Table

[\<table>  
&nbsp;&nbsp;\<tbody>  
&nbsp;&nbsp;&nbsp;&nbsp;\<tr>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td colspan="3"> Table Name \</td>    
&nbsp;&nbsp;&nbsp;&nbsp;\</tr>  
&nbsp;&nbsp;&nbsp;&nbsp;\<tr>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> #String data \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> Integer data \</td>    
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> Decimal data \</td>   
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> Double data \</td>   
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> Boolean data? \</td>   
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> DateTime data? \</td>   
&nbsp;&nbsp;&nbsp;&nbsp;\</tr>  
&nbsp;&nbsp;&nbsp;&nbsp;\<tr>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> Description \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> 12 \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> 12.5M \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> 12.5 \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> True \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> 2012-03-26 \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;\</tr>    
&nbsp;&nbsp;&nbsp;&nbsp;\<tr>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> Description \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> 12 \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> 12.5M \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> 12.5D \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> False \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> 2012-03-26 12:12:12\</td>  
&nbsp;&nbsp;&nbsp;&nbsp;\</tr>   
&nbsp;&nbsp;&nbsp;&nbsp;\<tr>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> Description \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> 12 \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> 12M \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> 12D \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> false \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> 2012-03-26 12:12:12\</td>  
&nbsp;&nbsp;&nbsp;&nbsp;\</tr>   
&nbsp;&nbsp;\</tbody>  
\</table>
](# "$decision_table")

| Parse decision table                                                                                                                  ||||||
| Decision Table       | Table Type? | Table Name? | Input Header?                           | Output Header?              | Comment Header? |
| -------------------- | ----------- | ----------- | --------------------------------------- | --------------------------- | --------------- |
| $decision_table      | Decision    | Table Name  | Integer data,Decimal data,Double data | Boolean data,DateTime data | String data     |


| Parse decision table data types                                                                           ||||||||
| Decision Table                  | Row No | Column 1? | Column 2? | Column 3? | Column 4? | Column 5? | Column 6? |
| ------------------------------- | ------ | --------- | --------- | --------- | --------- | --------- | --------- |
| $decision_table                 | 1      | string    | integer   | decimal   | double    | boolean   | datetime  |
| $decision_table                 | 2      | string    | integer   | decimal   | double    | boolean   | datetime  |
| $decision_table                 | 3      | string    | integer   | decimal   | double    | boolean   | datetime  |
| $decision_table                 | 0      | string    | integer   | decimal   | double    | boolean   | datetime  |
>we use Row #0 to designate the column type

| Parse decision table data                                                                                       ||||||||
| Decision Table            | Row No | Column 1?   | Column 2? | Column 3? | Column 4? | Column 5? | Column 6?           |
| ------------------------- | ------ | ----------- | --------- | --------- | --------- | --------- | ------------------- |
| $decision_table           | 1      | Description | 12        | 12.5M     | 12.5      | True      | 2012-03-26          |
| $decision_table           | 2      | Description | 12        | 12.5M     | 12.5D     | False     | 2012-03-26 12:12:12 |
| $decision_table           | 3      | Description | 12        | 12        | 12        | false     | 2012-03-26 12:12:12 |

## Decision Table with Variables
When decision table contains references to already defined variables, the table data parser needs to be able to recognize these variables and extract the correct type and value for the variables. If the table refers to variables not defined, the table data parser will interpret them as string.

Decision tables other than setup tables can only have reference to already defined variables. The Setup decision table can also create new variables as outputs.    

#### Decision table with reference to defined variables
Suppose we have two variables: [variable 1](#, "$variable_1") and [variable 2](#, "$variable_2").  
And the below decision table has references to the above variables:  
[\<table>  
&nbsp;&nbsp;\<tbody>  
&nbsp;&nbsp;&nbsp;&nbsp;\<tr>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td colspan="3"> Table Name \</td>    
&nbsp;&nbsp;&nbsp;&nbsp;\</tr>  
&nbsp;&nbsp;&nbsp;&nbsp;\<tr>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> #Column 1 \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> Column 2 \</td>    
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> Column 3? \</td>   
&nbsp;&nbsp;&nbsp;&nbsp;\</tr>  
&nbsp;&nbsp;&nbsp;&nbsp;\<tr>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> $variable_1 \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> 12 \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> $variable_2 \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;\</tr>    
&nbsp;&nbsp;&nbsp;&nbsp;\<tr>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> $variable_2 \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> $variable_1 \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> 12.5M \</td>   
&nbsp;&nbsp;&nbsp;&nbsp;\</tr>   
&nbsp;&nbsp;&nbsp;&nbsp;\<tr>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> Description \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> $variable_2 \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> $variable_1 \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;\</tr>   
&nbsp;&nbsp;\</tbody>  
\</table>
](# "$decision_table_variables")

| Parse decision table data types with variables                                                                                               ||||||
| Decision Table                                 | Variables                                           | Row No | Column 1? | Column 2? | Column 3? |
| ---------------------------------------------- | --------------------------------------------------- | ------ | --------- | --------- | --------- |
| $decision_table_variables                      | ["$variable_1:variable 1","$variable_2:variable 2"] | 1      | string    | integer   | string    |
| $decision_table_variables                      | ["$variable_1:variable 1","$variable_2:variable 2"] | 2      | string    | string    | decimal   |
| $decision_table_variables                      | ["$variable_1:variable 1","$variable_2:variable 2"] | 3      | string    | string    | string    |
| $decision_table_variables                      | ["$variable_1:variable 1","$variable_2:variable 2"] | 0      | string    | object    | object    |
>we use Row #0 to designate the column type

| Parse decision table data with variables                                                                                                   ||||||
| Decision Table                           | Variables                                           | Row No | Column 1?   | Column 2?  | Column 3?  |
| ---------------------------------------- | --------------------------------------------------- | ------ | ----------- | ---------- | ---------- |
| $decision_table_variables                | ["$variable_1:variable 1","$variable_2:variable 2"] | 1      | variable 1  | 12         | variable 2 |
| $decision_table_variables                | ["$variable_1:variable 1","$variable_2:variable 2"] | 2      | variable 2  | variable 1 | 12.5M      |
| $decision_table_variables                | ["$variable_1:variable 1","$variable_2:variable 2"] | 3      | Description | variable 2 | variable 1 |

#### Decision table with reference to variables not defined   
If the decision table has reference to a variable not defined, the parser will ignore the variable and interpret it as a string.  
[\<table>  
&nbsp;&nbsp;\<tbody>  
&nbsp;&nbsp;&nbsp;&nbsp;\<tr>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td colspan="3"> Table Name \</td>    
&nbsp;&nbsp;&nbsp;&nbsp;\</tr>  
&nbsp;&nbsp;&nbsp;&nbsp;\<tr>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> #Column 1 \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> Column 2 \</td>    
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> Column 3? \</td>   
&nbsp;&nbsp;&nbsp;&nbsp;\</tr>  
&nbsp;&nbsp;&nbsp;&nbsp;\<tr>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> $variable_1 \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> 12 \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> $variable_2 \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;\</tr>    
&nbsp;&nbsp;&nbsp;&nbsp;\<tr>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> $variable_2 \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> $variable_3 \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> 12.5M \</td>   
&nbsp;&nbsp;&nbsp;&nbsp;\</tr>   
&nbsp;&nbsp;&nbsp;&nbsp;\<tr>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> Description \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> $variable_2 \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> $variable_3 \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;\</tr>   
&nbsp;&nbsp;\</tbody>  
\</table>
](# "$decision_table_variables_not_defined")

| Parse decision table data types with variables not defined                                                                                               ||||||
| Decision Table                                             | Variables                                           | Row No | Column 1? | Column 2? | Column 3? |
| ---------------------------------------------------------- | --------------------------------------------------- | ------ | --------- | --------- | --------- |
| $decision_table_variables_not_defined                      | ["$variable_1:variable 1","$variable_2:variable 2"] | 1      | string    | integer   | string    |
| $decision_table_variables_not_defined                      | ["$variable_1:variable 1","$variable_2:variable 2"] | 2      | string    | string    | decimal   |
| $decision_table_variables_not_defined                      | ["$variable_1:variable 1","$variable_2:variable 2"] | 3      | string    | string    | string    |
| $decision_table_variables_not_defined                      | ["$variable_1:variable 1","$variable_2:variable 2"] | 0      | string    | object    | object    |
>we use Row #0 to designate the column type

| Parse decision table data with variables not defined                                                                                                         ||||||
| Decision Table                                       | Variables                                           | Row No | Column 1?   | Column 2?     | Column 3?     |
| ---------------------------------------------------- | --------------------------------------------------- | ------ | ----------- | ------------- | ------------- |
| $decision_table_variables_not_defined                | ["$variable_1:variable 1","$variable_2:variable 2"] | 1      | variable 1  | 12            | variable 2    |
| $decision_table_variables_not_defined                | ["$variable_1:variable 1","$variable_2:variable 2"] | 2      | variable 2  | "$variable_3" | 12.5M         |
| $decision_table_variables_not_defined                | ["$variable_1:variable 1","$variable_2:variable 2"] | 3      | Description | variable 2    | "$variable_3" |

## Setup decision table with variable
If the decision table is a setup type, it can define new variables in outputs in addition to refer to existing variables in inputs   

In the following setup table, it can still refer to `$variable_1` and `$variable_2` but more importantly it defines two new variables in the output column `$variable_3` and `$variable_4`. Variables defined in the setup table are always of type `object`  
[\<table>  
&nbsp;&nbsp;\<tbody>  
&nbsp;&nbsp;&nbsp;&nbsp;\<tr>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td colspan="3"> setup:Table Name \</td>    
&nbsp;&nbsp;&nbsp;&nbsp;\</tr>  
&nbsp;&nbsp;&nbsp;&nbsp;\<tr>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> #Column 1 \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> Column 2 \</td>    
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> Column 3? \</td>   
&nbsp;&nbsp;&nbsp;&nbsp;\</tr>  
&nbsp;&nbsp;&nbsp;&nbsp;\<tr>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> $variable_1 \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> 12 \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> $variable_3 \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;\</tr>    
&nbsp;&nbsp;&nbsp;&nbsp;\<tr>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> $variable_2 \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> $variable_1 \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> 12.5M \</td>   
&nbsp;&nbsp;&nbsp;&nbsp;\</tr>   
&nbsp;&nbsp;&nbsp;&nbsp;\<tr>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> Description \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> 12 \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> $variable_4 \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;\</tr>   
&nbsp;&nbsp;\</tbody>  
\</table>
](# "$setup_decision_table_variables")

| Parse setup decision table data types with variables                                                                                               ||||||
| Decision Table                                       | Variables                                           | Row No | Column 1? | Column 2? | Column 3? |
| ---------------------------------------------------- | --------------------------------------------------- | ------ | --------- | --------- | --------- |
| $setup_decision_table_variables                      | ["$variable_1:variable 1","$variable_2:variable 2"] | 1      | string    | integer   | object    |
| $setup_decision_table_variables                      | ["$variable_1:variable 1","$variable_2:variable 2"] | 2      | string    | string    | decimal   |
| $setup_decision_table_variables                      | ["$variable_1:variable 1","$variable_2:variable 2"] | 3      | string    | integer   | object    |
| $setup_decision_table_variables                      | ["$variable_1:variable 1","$variable_2:variable 2"] | 0      | string    | object    | object    |
>we use Row #0 to designate the column type

| Parse setup decision table data with variables                                                                                                  ||||||
| Decision Table                                 | Variables                                           | Row No | Column 1?   | Column 2?  | Column 3? |
| ---------------------------------------------- | --------------------------------------------------- | ------ | ----------- | ---------- | --------- |
| $setup_decision_table_variables                | ["$variable_1:variable 1","$variable_2:variable 2"] | 1      | variable 1  | 12         | null      |
| $setup_decision_table_variables                | ["$variable_1:variable 1","$variable_2:variable 2"] | 2      | variable 2  | variable 1 | 12.5M     |
| $setup_decision_table_variables                | ["$variable_1:variable 1","$variable_2:variable 2"] | 3      | Description | 12         | null      |
