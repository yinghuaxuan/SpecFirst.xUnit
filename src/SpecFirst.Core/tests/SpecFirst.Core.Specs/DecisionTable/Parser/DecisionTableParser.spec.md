﻿The decision table parser takes the html output of the markdown table and parse it to a decision table object.

## Table Type, Name, and Header Parser
Given the following valid decision tables:

1. Decision table  
[\<table>  
&nbsp;&nbsp;\<tbody>  
&nbsp;&nbsp;&nbsp;&nbsp;\<tr>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td colspan="3"> Table Name \</td>    
&nbsp;&nbsp;&nbsp;&nbsp;\</tr>  
&nbsp;&nbsp;&nbsp;&nbsp;\<tr>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> #Description \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> Table Header 1 \</td>    
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> Table Header 2? \</td>   
&nbsp;&nbsp;&nbsp;&nbsp;\</tr>  
&nbsp;&nbsp;&nbsp;&nbsp;\<tr>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> Description \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> Table Data 1 \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> Table Data 2 \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;\</tr>    
&nbsp;&nbsp;\</tbody>  
\</table>
](# "$decision_table_default")

2. Decision table  
[\<table>  
&nbsp;&nbsp;\<tbody>  
&nbsp;&nbsp;&nbsp;&nbsp;\<tr>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td colspan="3"> decision:Table Name \</td>    
&nbsp;&nbsp;&nbsp;&nbsp;\</tr>  
&nbsp;&nbsp;&nbsp;&nbsp;\<tr>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> #Description \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> Table Header 1 \</td>    
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> Table Header 2? \</td>   
&nbsp;&nbsp;&nbsp;&nbsp;\</tr>  
&nbsp;&nbsp;&nbsp;&nbsp;\<tr>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> Description \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> Table Data 1 \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> Table Data 2 \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;\</tr>    
&nbsp;&nbsp;\</tbody>  
\</table>
](# "$decision_table")

3. Decision table with th headers  
[\<table>  
&nbsp;&nbsp;\<thead>  
&nbsp;&nbsp;&nbsp;&nbsp;\<tr>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td colspan="3"> Decision:Table Name \</td>    
&nbsp;&nbsp;&nbsp;&nbsp;\</tr>  
&nbsp;&nbsp;&nbsp;&nbsp;\<tr>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<th> #Description \</th>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<th> Table Header 1 \</th>    
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<th> Table Header 2? \</th>      
&nbsp;&nbsp;&nbsp;&nbsp;\</tr>  
&nbsp;&nbsp;\</thead>  
&nbsp;&nbsp;\<tbody>  
&nbsp;&nbsp;&nbsp;&nbsp;\<tr>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> Description \</td>    
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> Table Data 1 \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> Table Data 2 \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;\</tr>    
&nbsp;&nbsp;\</tbody>  
\</table>
](# "$decision_table_with_theader")

4. Comment decision table  
[\<table>  
&nbsp;&nbsp;\<tbody>  
&nbsp;&nbsp;&nbsp;&nbsp;\<tr>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td colspan="3"> comment:Table Name \</td>    
&nbsp;&nbsp;&nbsp;&nbsp;\</tr>  
&nbsp;&nbsp;&nbsp;&nbsp;\<tr>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> #Description \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> Table Header 1 \</td>    
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> Table Header 2? \</td>    
&nbsp;&nbsp;&nbsp;&nbsp;\</tr>  
&nbsp;&nbsp;&nbsp;&nbsp;\<tr>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> Description \</td>    
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> Table Data 1 \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> Table Data 2 \</td>   
&nbsp;&nbsp;&nbsp;&nbsp;\</tr>    
&nbsp;&nbsp;\</tbody>  
\</table>
](# "$comment_decision_table")

5. Setup decision table  
[\<table>  
&nbsp;&nbsp;\<tbody>  
&nbsp;&nbsp;&nbsp;&nbsp;\<tr>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td colspan="3"> setup:Table Name \</td>    
&nbsp;&nbsp;&nbsp;&nbsp;\</tr>  
&nbsp;&nbsp;&nbsp;&nbsp;\<tr>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> #Description \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> Table Header 1 \</td>    
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> Table Header 2? \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;\</tr>  
&nbsp;&nbsp;&nbsp;&nbsp;\<tr>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> Description \</td>    
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> Table Data 1 \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> Table Data 2 \</td>   
&nbsp;&nbsp;&nbsp;&nbsp;\</tr>    
&nbsp;&nbsp;\</tbody>  
\</table>
](# "$setup_decision_table")

| Parse a decision table                                                                                                                                   |||||||
| #Description                   | Decision Table               | Table Type? | Table Name? | Input Table Header? | Output Table Header? | Comment Table Header? |
| ------------------------------ | ---------------------------- | ----------- | ----------- | ------------------- | -------------------- | --------------------- |
| Decision table                 | $decision_table_default      | decision    | Table Name  | Table Header 1      | Table Header 2       | Description           |
| Decision table                 | $decision_table              | decision    | Table Name  | Table Header 1      | Table Header 2       | Description           |
| Decision table with th headers | $decision_table_with_theader | decision    | Table Name  | Table Header 1      | Table Header 2       | Description           |
| Comment decision table         | $comment_decision_table      | comment     | Table Name  | Table Header 1      | Table Header 2       | Description           |
| Setup decision table           | $setup_decision_table        | setup       | Table Name  | Table Header 1      | Table Header 2       | Description           |

## Table Data Parser

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
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> Boolean data \</td>   
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> DateTime data \</td>   
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
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> 12 \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> 12 \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> false \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> 2012-03-26 12:12:12\</td>  
&nbsp;&nbsp;&nbsp;&nbsp;\</tr>   
&nbsp;&nbsp;\</tbody>  
\</table>
](# "$decision_table_data")

| Parse decision table data types                                                                           ||||||||
| Decision Table                  | Row No | Column 1? | Column 2? | Column 3? | Column 4? | Column 5? | Column 6? |
| ------------------------------- | ------ | --------- | --------- | --------- | --------- | --------- | --------- |
| $decision_table_data            | 1      | string    | integer   | decimal   | double    | boolean   | datetime  |
| $decision_table_data            | 2      | string    | integer   | decimal   | double    | boolean   | datetime  |
| $decision_table_data            | 3      | string    | integer   | decimal   | double    | boolean   | datetime  |
| $decision_table_data            | 0      | string    | integer   | decimal   | double    | boolean   | datetime  |


| Parse decision table data                                                                                       ||||||||
| Decision Table            | Row No | Column 1?   | Column 2? | Column 3? | Column 4? | Column 5? | Column 6?           |
| ------------------------- | ------ | ----------- | --------- | --------- | --------- | --------- | ------------------- |
| $decision_table_data      | 1      | Description | 12        | 12.5M     | 12.5      | True      | 2012-03-26          |
| $decision_table_data      | 2      | Description | 12        | 12.5M     | 12.5D     | False     | 2012-03-26 12:12:12 |
| $decision_table_data      | 3      | Description | 12        | 12        | 12        | false     | 2012-03-26 12:12:12 |


>we use Row #0 to designate the column type

## Decision table with variable
Normal decision table can have reference to already defined variables in the data.   

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

| Parse decision table data types with variables                                          |||||
| Decision Table                                 | Row No | Column 1? | Column 2? | Column 3? |
| ---------------------------------------------- | ------ | --------- | --------- | --------- |
| $decision_table_data                           | 1      | string    | integer   | string    |
| $decision_table_data                           | 2      | string    | string    | decimal   |
| $decision_table_data                           | 3      | string    | string    | string    |
| $decision_table_data                           | 0      | string    | object    | object    |


| Parse decision table data with variables                                              |||||
| Decision Table                           | Row No | Column 1?   | Column 2?  | Column 3?  |
| ---------------------------------------- | ------ | ----------- | ---------- | ---------- |
| $decision_table_data                     | 1      | variable_1  | 12         | variable_2 |
| $decision_table_data                     | 2      | variable_2  | variable_1 | 12.5M      |
| $decision_table_data                     | 3      | Description | variable_2 | variable_1 |

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

| Parse decision table data types with variables not defined                                          |||||
| Decision Table                                             | Row No | Column 1? | Column 2? | Column 3? |
| ---------------------------------------------------------- | ------ | --------- | --------- | --------- |
| $decision_table_variables_not_defined                      | 1      | string    | integer   | string    |
| $decision_table_variables_not_defined                      | 2      | string    | string    | decimal   |
| $decision_table_variables_not_defined                      | 3      | string    | string    | string    |
| $decision_table_variables_not_defined                      | 0      | string    | object    | object    |


| Parse decision table data with variables not defined                                                |||||
| Decision Table                                       | Row No | Column 1?   | Column 2?   | Column 3?   |
| ---------------------------------------------------- | ------ | ----------- | ----------- | ----------- |
| $decision_table_variables_not_defined                | 1      | variable_1  | 12          | variable_2  |
| $decision_table_variables_not_defined                | 2      | variable_2  | $variable_3 | 12.5M       |
| $decision_table_variables_not_defined                | 3      | Description | variable_2  | $variable_3 |


## Setup decision table with variable
If the decision table is a setup type, it can both refer to existing variables and define new variables.  

In the following setup table, it can still refer to `$variable_1` and `$variable_2` but more importantly it defines two new variables in the output column `$variable_3` and `$variable_4`
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

| Parse setup decision table data types with variables                                          |||||
| Decision Table                                       | Row No | Column 1? | Column 2? | Column 3? |
| ---------------------------------------------------- | ------ | --------- | --------- | --------- |
| $decision_table_variables_not_defined                | 1      | string    | integer   | object    |
| $decision_table_variables_not_defined                | 2      | string    | string    | decimal   |
| $decision_table_variables_not_defined                | 3      | string    | integer   | object    |
| $decision_table_variables_not_defined                | 0      | string    | object    | object    |


| Parse setup decision table data with variables                                                |||||
| Decision Table                                 | Row No | Column 1?   | Column 2?   | Column 3?   |
| ---------------------------------------------- | ------ | ----------- | ----------- | ----------- |
| $decision_table_variables_not_defined          | 1      | variable_1  | 12          | variable_2  |
| $decision_table_variables_not_defined          | 2      | variable_2  | $variable_3 | 12.5M       |
| $decision_table_variables_not_defined          | 3      | Description | variable_2  | $variable_3 |
