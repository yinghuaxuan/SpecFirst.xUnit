Decision variables are text start with $ symbol, for example $variable_name is a variable named variable_name.

Decision variables can be defined in a couple of ways:
- Link  
For example, [this is a variable](# "$variable_name") defines a variable called variable_name. Put your cursor over the link will reveal the variable name. Variable defined in the link will be of type string.
- Decision table  
If the text inside the decision table begin with a $ symbol, it will be interpreted as a variable. If the variable is part of the input, it means the variable has been defined somewhere else; if the variable is part of the output, it is a new variable. Variable defined in decision table will be of type object. 

Outside Link and Decision table, the $ symbol won't be interpreted as variable.

| Parse decision variable from links                                                                                                                                                                 ||||||
| #Description                                         | Text                                                                  | Contain variable? | Variable name? | Variable type? | Variable value?    |
| ---------------------------------------------------- | --------------------------------------------------------------------- | ----------------- | -------------- | -------------- | ------------------ |
| variable must start with $ symbol followed by letter | <a href="" title="$variable_name" data-href="">this is a variable</a> | true              | variable_name  | string         | this is a variable |
| variable must start with $ symbol followed by letter | <a href="" title="$variable123" data-href="">this is a variable</a>   | true              | variable123    | string         | this is a variable |
| variable immediately followed by digit is not valid  | <a href="" title="$123" data-href="">this is a variable</a>           | false             |                |                |                    |
| variable not start with $ symbol is not valid        | <a href="" title="variable_name" data-href="">this is a variable</a>  | false             |                |                |                    |
| variable with $ symbol not at the start is not valid | <a href="" title="variable_$name" data-href="">this is a variable</a> | false             |                |                |                    |



