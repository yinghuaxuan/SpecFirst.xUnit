Decision table is one of the slim tables defined in FitNesse, which "Supplies the inputs and outputs for decisions. This is similar to the Fit Column Fixture". More details can be found on [FitNesse](http://fitnesse.org/FitNesse.UserGuide.WritingAcceptanceTests.SliM.DecisionTable) website.

## Decision Table Validator
In SpecFirst, we use the rules defined in [DecisionTableValidator](Validator\DecisionTableValidator.spec.md) to validate whether a table is a decision table.

## Decision Table Types
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
If the setup table is only meant for a particular table or tables, it should specify all the target tables in the column `Target` with a comma separated list of all tables it is targeting.  


