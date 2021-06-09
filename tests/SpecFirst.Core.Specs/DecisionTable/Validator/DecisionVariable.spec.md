Decision variables are text start with $ symbol, for example $variable_name is a variable named variable_name.

Decision variables can be defined in a couple of ways:
- Link  
For example, [this is a variable](# "$variable_name") defines a variable called variable_name. Put your cursor over the link will reveal the variable name. Variable defined in the link will be of type string.
- Decision table
If the text inside the decision table begin with a $ symbol, it will be interpreted as a variable. If the variable is part of the input, it means the variable has been defined somewhere else; if the variable is part of the output, it is a new variable. Variable defined in decision table will be of type object. 

Outside Link and Decision table, the $ symbol won't be interpreted as variable.

| Parse decision variable from text                                                                                                                                         ||||||                    |
| #Description                                             | Text                                     | In decision table? | Contain variable? | Variable name? | Variable type? | Variable value?    |
| -------------------------------------------------------- | ---------------------------------------- | ------------------ | ----------------- | -------------- | -------------- | ------------------ |
| in a link, with $ symbol                                 | "[this is a variable](# "$variable_name")" | false              | true              | variable_name  | string         | this is a variable |
| variable name must start with letter                     | [this is a variable](# "$123")           | false              | fase              |                |                |                    |
| in a link, without $ symbol                              | [this is a variable](# "variable_name")  | false              | false             |                |                |                    |
| in a link, $ symbol not at the start                     | [this is a variable](# "variable_$name") | false              | false             |                |                |                    |
| if a link is in decision table, variable won't be parsed | [this is a variable](# "$variable_name") | true               | false             |                |                |                    |
| in a decision table, with $ symbol                       | $variable_name                           | true               | true              | variable_name  | object         |                    |
| in a decision table, with $ symbol                       | $123                                     | true               | false             |                |                |                    |
| in a decision table, without $ symbol                    | variable_name                            | true               | false             |                |                |                    |
| in a decision table, $ symbol not at the start           | variable_$name                           | true               | false             |                |                |                    |


