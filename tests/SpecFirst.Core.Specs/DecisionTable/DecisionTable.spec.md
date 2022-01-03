Decision table is one of the slim tables defined in FitNesse, which "Supplies the inputs and outputs for decisions. This is similar to the Fit Column Fixture". More details can be found on [FitNesse](http://fitnesse.org/FitNesse.UserGuide.WritingAcceptanceTests.SliM.DecisionTable) website.

## Decision Table Structure
The first row of the table can only have one column, which contains table type (optional) and table name. The table name will be translated into the test class.
The second row of the table defines all the input and output paramters for each test. It can also include comment parameters to describe the scenario.
The third row and onwards define all the test scenarios. Each row is a test scenario.

In SpecFirst, we use the rules defined in [DecisionTableValidator](Validator\DecisionTableValidator.spec.md) to decide whether a table is a decision table.

## Decision Table Types
SpecFirst defines three types of decision tables - decision, setup, and comment.  

The type is defined by prefix to the table name:  
- If the table name has no prefix or prefixed with ```decision``` (case insensitive), it is a decision table
- If the table name is prefixed with ```setup``` (case insensitive), it is a setup decision table
- If the table name is prefixed with ```comment``` (case insensitive), it is a comment decision table

## Decision table
Normal decision tables are used to generate tests.

## Comment decision table
Comment decision tables are similar to the normal decision table - the only difference is that they must have table type `comment`. Comment decision tables won't be included for the test generation so they are useful when you have tables in draft status and you don't want tests to be generated yet.

