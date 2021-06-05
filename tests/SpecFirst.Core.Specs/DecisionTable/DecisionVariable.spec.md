Decision variables are text start with $ symbol, for example $variable_name is a variable named variable_name.

Decision variables can be defined in a couple of ways:
- Link  
For example, [this is a variable](# "$variable_name") defines a variable called variable_name. Put your cursor over the link will reveal the variable name. Variable defined in the link will be of type string.
- Decision table  
If the text inside the decision table begin with a $ symbol, it will be interpreted as a variable. If the variable is part of the input, it means the variable has been defined somewhere else; if the variable is part of the output, it is a new variable. Variable defined in decision table will be of type object. 

Outside Link and Decision table, the $ symbol won't be interpreted as variable.

| Parse decision variable from links                                                                                                                                    ||||||
| #Description                                         | Text                                     | Contain variable? | Variable name? | Variable type? | Variable value?    |
| ---------------------------------------------------- | ---------------------------------------- | ----------------- | -------------- | -------------- | ------------------ |
| variable must start with $ symbol followed by letter | [this is a variable](# "$variable_name") | true              | variable_name  | string         | this is a variable |
| variable must start with $ symbol followed by letter | [this is a variable](# "$variable123")   | true              | variable_name  | string         | this is a variable |
| variable immediately followed by digit               | [this is a variable](# "$123")           | fase              |                |                |                    |
| variable not start with $ symbol                     | [this is a variable](# "variable_name")  | false             |                |                |                    |
| variable with $ symbol not at the start              | [this is a variable](# "variable_$name") | false             |                |                |                    |


| Parse decision variable from tables                                                                                                                  |||||||
| #Description                                         | Text           | In header? | Contain variable? | Variable name? | Variable type? | Variable value? |
| ---------------------------------------------------- | -------------- | ---------- | ----------------- | -------------- | -------------- | --------------- |
| variable must start with $ symbol followed by letter | $variable_name | true       | true              | variable_name  | object         |                 |
| variable must start with $ symbol followed by letter | $variable123   | true       | true              | variable123    | object         |                 |
| variable immediately followed by digit               | $123           | true       | false             |                |                |                 |
| variable not start with $ symbol                     | variable_name  | true       | false             |                |                |                 |
| variable with $ symbol not at the start              | variable_$name | true       | false             |                |                |                 |




