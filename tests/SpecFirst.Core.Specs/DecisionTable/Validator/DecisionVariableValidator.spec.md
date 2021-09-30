Decision variables are text start with $ symbol, for example $variable_name is a variable named variable_name.

### Define a new decision variable  
Decision variables can be defined in a couple of ways:
- As a link  
For example, [this is a variable](# "$variable_name") defines a variable called variable_name. Put your cursor over the link will reveal the variable name.  
Variable defined in the link will always be of type `string`.
- In a setup decision table  
In a setup decision variable, any $ text in the **output** columns that can be interpreted as a valid variable will be interpreted as a new variable.  
Variable defined in the setup decision table will always be of type `object`.

### Refer to an existing decision variable  
Decision variables can only be refered to in a decision table:  
- In a decision table other than Setup decision table, decision variables can be referred to in any type of columns (comment, input and output)
- In a Setup decision table, decision variables can only be referred to in comment and input columns. This is because decision variables in output columns will be interpreted as new variables

> you can't refer to decision variables outside decision tables!

### Validate a decision variable

| Validate decision variables from links                                                                                                                                                                   ||||||
| #Description                                         | Text                                                                    | Contain variable? | Variable name? | Variable type? | Variable value?    |
| ---------------------------------------------------- | ----------------------------------------------------------------------- | ----------------- | -------------- | -------------- | ------------------ |
| variable must start with $ symbol followed by letter | \<a href="" title="$variable_name" data-href="">this is a variable\</a> | true              | variable_name  | string         | this is a variable |
| variable must start with $ symbol followed by letter | \<a href="" title="$variable123" data-href="">this is a variable\</a>   | true              | variable123    | string         | this is a variable |
| variable start with digit is not valid               | \<a href="" title="$123" data-href="">this is a variable\</a>           | false             |                |                |                    |
| variable not start with $ symbol is not valid        | \<a href="" title="variable_name" data-href="">this is a variable\</a>  | false             |                |                |                    |
| variable with $ symbol not at the start is not valid | \<a href="" title="variable_$name" data-href="">this is a variable\</a> | false             |                |                |                    |
