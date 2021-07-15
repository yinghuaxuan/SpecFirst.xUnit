Decision table is one of the slim tables defined in FitNesse, which "Supplies the inputs and outputs for decisions. This is similar to the Fit Column Fixture". More details can be found on FitNesse website http://fitnesse.org/FitNesse.UserGuide.WritingAcceptanceTests.SliM.DecisionTable.

In SpecFirst, we use the rules defined in [DecisionTableValidator](DecisionTableValidator.spec.md) to validate whether a table is a decision table.

## Table Type
SpecFirst defines three types of decision tables - decision, setup, and comment.  

The type is defined by prefix to the table name:  
- If the table name has no prefix or prefixed with ```decision``` (case insensitive), it is a decision table
- If the table name is prefixed with ```setup``` (case insensitive), it is a setup decision table
- If the table name is prefixed with ```comment``` (case insensitive), it is a comment decision table

### Setup decision table
Setup decision table is mainly used for setting up the common logics for the tests. The setup can be done in 3 scopes:
- Table Row
The setup logic should be run for each row (test) of the tables. This is the default scope.
- Table
The setup logic should be run for each table (test class). This is equivalent to the class fixture in xUnit.
- Page
The setup logic should be run only once for the page. This is equivalent to the collection fixture in xUnit.

A spec can have multiple setup decision tables in it.  

The scope should be defined with a special column named `Scope` in the setup table.  
If there is no `Scope` column found in the setup table, the table will be run for every table in the spec file.  
If the setup table is only meant for a particular table or tables, it should specify all the target tables in the column `Target` with a comma separated list.  

### Comment decision table
Commented decision tables won't participate in the tests generation.

## Table Name
Table name is in the #1 row of the table. The #1 row can also contain table type in the format of ```type:name```.  
Table name will be generated as test class name so it shouldn't contain any invalid characters for a class name in the targeted langugage.

## Table Header
Table headers are in the row #2 of the table.  

There are three types of headers in decision table:
- Input column
- Output column (output columns are suffixed by '?')
- Comment column (comment columns are prefixed with '#')

A decision table can have any number of input columns, any number of output columns and any number of comment columns. However, it must have at least one input column or one output column. For example, a table with only comment columns won't be considered as a valid decision table.

Input columns will be generated as input parameters for the test method and therefore they should contain any invalid characters for a method parameter in the targeted langugage.  
Output columns will be generated as return parameters and therefore they should contain any invalid characters for a method parameter in the targeted langugage.   
Comment columns will be added as code comments to the tests.

## Table Data
Table data are from row #3 and onwards.
