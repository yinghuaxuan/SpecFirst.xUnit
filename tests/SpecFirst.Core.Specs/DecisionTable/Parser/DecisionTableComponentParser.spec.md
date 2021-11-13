The decision table has a few components - table type, table name, table header, and table data. We create a parser for each component:
- Table Type Parser
- Table Name Parser
- Table Header Parser
- Table Data Parser 

In the following sections, we are going to look at each parser and see what they do and how they work. But before that, let's create a few sample decision tables for illustrations.

## A few sample decision tables
We create a few sample decision tables in HTML that are going to be used for testing all the parsers.

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

2. Decision table with 'decision' prefix to table name  
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

## Table Type and Name Parser
The table type and name are specified in the first row of the decision table in the format of `type:name`.  

There are three table types and they are defined by the corresponding word prefixed to the table name:
- decision (this is the default table type if there is no prefix; the word 'decision' is case insensitive)
- comment (table name prefixed by word 'comment'; case insensitive)
- setup (table name prefixed by word 'setup'; case insensitive)

| Parse decision table name and type                                                         ||||
| #Description                       | Decision Table               | Table Type? | Table Name? |
| ---------------------------------- | ---------------------------- | ----------- | ----------- |
| Decision table without prefix      | $decision_table_default      | Decision    | Table Name  |
| Decision table                     | $decision_table              | Decision    | Table Name  |
| Decision table with th headers     | $decision_table_with_theader | Decision    | Table Name  |
| Comment decision table             | $comment_decision_table      | Comment     | Table Name  |
| Setup decision table               | $setup_decision_table        | Setup       | Table Name  |

## Table Header Parser
Table headers are specified in the second row of the decision table. The Table Header Parser generates a `TableHeader` object that contains the header name, header types, as well as header decorations.  

The `TableHeader` object also contains the most compatible data type for all the data in the column. However, the data type are extracted from the data in the column, not from the header text.  

### Table Header Types
There are three types of table headers:
- comment table header (prefixed by '#' symbol to the table header name)
- output table header (suffixed by '?' symbol to the table header name)
- input table header

### Table Header Decorations
Table headers can be decorated with additional information in them (in the format of links). These decorations are used to pass more information (as in the title of the link) into the parser.   
For example, a header can specify one or more flags to say that cases or spaces or line endings should be ignroed when processing data for the header/column:  
- ignore_case
- ignore_all_spaces
- ignore_line_endings

When there are multiple flags, they need to be separated by '|' pipe.  

Flags specified in the comment table headers will be ignored.  

Below defines a couple more decision tables with headers decorated with more information:  

6. Decision table with decorations in headers  
[\<table>  
&nbsp;&nbsp;\<thead>  
&nbsp;&nbsp;&nbsp;&nbsp;\<tr>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td colspan="3"> Decision:Table Name \</td>    
&nbsp;&nbsp;&nbsp;&nbsp;\</tr>  
&nbsp;&nbsp;&nbsp;&nbsp;\<tr>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<th> #\<a href="#" title="ignore_case" data-href="#">Description<\/a> \</th>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<th> \<a href="#" title="ignore_case|ignore_all_spaces" data-href="#">Table Header 1<\/a> \</th>    
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<th> \<a href="#" title="ignore_case|ignore_line_ending" data-href="#">Table Header 2<\/a>? \</th>      
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
](# "$decision_table_with_decorations_in_headers")

7. Decision table with links in th headers  
[\<table>  
&nbsp;&nbsp;\<thead>  
&nbsp;&nbsp;&nbsp;&nbsp;\<tr>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td colspan="3"> Decision:Table Name \</td>    
&nbsp;&nbsp;&nbsp;&nbsp;\</tr>  
&nbsp;&nbsp;&nbsp;&nbsp;\<tr>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<th> \<a href="#" title="ignore_case" data-href="#">#Description<\/a> \</th>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<th> \<a href="#" title="ignore_case" data-href="#">Table Header 1<\/a> \</th>    
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<th> \<a href="#" title="ignore_case|ignore_line_ending" data-href="#">Table Header 2?<\/a> \</th>      
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
](# "$decision_table_with_decorations_in_th_headers")

| Parse decision table headers                                                                                                    |||||
| #Description                   | Decision Table                                 | Input Header?  | Output Header? | Comment Header? |
| ------------------------------ | ---------------------------------------------- | -------------- | -------------- | --------------- |
| Decision table without prefix  | $decision_table_default                        | Table Header 1 | Table Header 2 | Description     |
| Decision table                 | $decision_table                                | Table Header 1 | Table Header 2 | Description     |
| Decision table with th headers | $decision_table_with_theader                   | Table Header 1 | Table Header 2 | Description     |
| Comment decision table         | $comment_decision_table                        | Table Header 1 | Table Header 2 | Description     |
| Setup decision table           | $setup_decision_table                          | Table Header 1 | Table Header 2 | Description     |
| Headers with decorations       | $decision_table_with_decorations_in_headers    | Table Header 1 | Table Header 2 | Description     |
| th headers with decorations    | $decision_table_with_decorations_in_th_headers | Table Header 1 | Table Header 2 | Description     |


| Parse header decorations                                                                                                                                                                  |||||
| #Description                           | Decision Table                                 | Input Header Info?                   | Output Header Info?                   | Comment Header Info? |
| -------------------------------------- | ---------------------------------------------- | ------------------------------------ | ------------------------------------- | -------------------- |
| No decorations defined                 | $decision_table_default                        |                                      |                                       |                      |
| No decorations defined                 | $decision_table                                |                                      |                                       |                      |
| No decorations defined                 | $decision_table_with_theader                   |                                      |                                       |                      |
| No decorations defined                 | $comment_decision_table                        |                                      |                                       |                      |
| No decorations defined                 | $setup_decision_table                          |                                      |                                       |                      |
| Decorations in comment headers ignored | $decision_table_with_decorations_in_headers    | ["ignore_case", "ignore_all_spaces"] | ["ignore_case", "ignore_line_ending"] |                      |
| Decorations in comment headers ignored | $decision_table_with_decorations_in_th_headers | ["ignore_case"]                      | ["ignore_case", "ignore_line_ending"] |                      |

## Table Data Parser
Table data are from the third row and onwards. The Table Data Parser parse the table data text into correct types and values.  

### Supported Data Types
Table data can contain the following types of data:
- `null` data (if a table cell contains nothing or contains text 'null')
- integer data
- decimal data
- double data
- boolean data
- datetime data
- string data
- decision variable
- collection 

The Table Data Parser utilize the [CollectionTypeResolver](TypeResolver\CollectionTypeResolver.spec.md) and [PrimitiveTypeResolver](TypeResolver\PrimitiveTypeResolver.spec.md) to parse the individual data.

### Data Normalization
When the parser works, it normalize the data text:
- Any white spaces before and after the text are ignored
- if there is no text, it will be treated as `null`

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
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> 12M \</td>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\<td> 12D \</td>  
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


| Parse decision table data                                                                                       ||||||||
| Decision Table            | Row No | Column 1?   | Column 2? | Column 3? | Column 4? | Column 5? | Column 6?           |
| ------------------------- | ------ | ----------- | --------- | --------- | --------- | --------- | ------------------- |
| $decision_table_data      | 1      | Description | 12        | 12.5M     | 12.5      | True      | 2012-03-26          |
| $decision_table_data      | 2      | Description | 12        | 12.5M     | 12.5D     | False     | 2012-03-26 12:12:12 |
| $decision_table_data      | 3      | Description | 12        | 12        | 12        | false     | 2012-03-26 12:12:12 |

### Decision table with variables
Decision table can contain references to decision variables. The Table Data Parser will recognize the variable as type `DecisionVariable` with data as `null`.      

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

| Parse decision table data types with variables                                                                                                                    ||||||
| Decision Table                                 | Row No | Column 1?        | Column 2?        | Column 3?        |
| ---------------------------------------------- | ------ | ---------------- | ---------------- | ---------------- |
| $decision_table_variables                      | 1      | DecisionVariable | integer          | DecisionVariable |
| $decision_table_variables                      | 2      | DecisionVariable | DecisionVariable | decimal          |
| $decision_table_variables                      | 3      | string           | DecisionVariable | DecisionVariable |


| Parse decision table data with variables                                                                                                 ||||||
| Decision Table                           | Row No | Column 1?   | Column 2? | Column 3? |
| ---------------------------------------- | ------ | ----------- | --------- | --------- |
| $decision_table_variables                | 1      | null        | 12        | null      |
| $decision_table_variables                | 2      | null        | null      | 12.5M     |
| $decision_table_variables                | 3      | Description | null      | null      |
