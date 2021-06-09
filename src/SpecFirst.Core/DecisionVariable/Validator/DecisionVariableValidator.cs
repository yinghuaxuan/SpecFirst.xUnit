using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace SpecFirst.Core.DecisionVariable.Validator
{
    public class DecisionVariableValidator
    {
        public bool Validate(XElement document, out IEnumerable<string> errors)
        {
            var errorList = new List<string>();

            XAttribute title = document.Attribute("title");
            if(title == null)
            {
                errorList.Add("Decision variable must have link title");
            }
            else
            {
                string value = title!.Value;
                if (!Validate(value, out var errorOutput))
                {
                    errorList.AddRange(errorOutput);
                }
            }

            errors = errorList;
            return !errors.Any();
        }

        public bool Validate(string value, out IEnumerable<string> errors)
        {
            var errorList = new List<string>();

            if (string.IsNullOrWhiteSpace(value))
            {
                errorList.Add("The link title can't be empty for decision variable");
            }
            else if (!value.StartsWith("$"))
            {
                errorList.Add("A decision variable must start with $ symbol");
            }
            else if (value.Length < 2)
            {
                errorList.Add("A decision variable must have at least 1 character after $ symbol");
            }
            else if (!char.IsLetter(value.Skip(1).Take(1).First()))
            {
                errorList.Add("A decision variable must have a letter following the $ symbol");
            }

            errors = errorList;
            return !errors.Any();
        }
    }
}
