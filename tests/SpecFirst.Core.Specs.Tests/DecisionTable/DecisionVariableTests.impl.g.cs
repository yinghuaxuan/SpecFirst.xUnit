
namespace SpecFirst.Core.Specs.Tests
{
    using System;
    using System.Xml.Linq;
    using SpecFirst.Core.DecisionVariable;
    using SpecFirst.Core.DecisionVariable.Parser;
    using SpecFirst.Core.DecisionVariable.Validator;

    public partial class parse_decision_variable_from_links
    {
        DecisionVariableValidator _validator;
        DecisionVariableParser _parser;

        public parse_decision_variable_from_links()
        {
            _validator = new DecisionVariableValidator();
            _parser = new DecisionVariableParser();
        }
        private partial (bool, string, string, string) parse_decision_variable_from_links_implementation(string text)
        {
            var link = XElement.Parse(text);
            var contain_variable = _validator.Validate(link, out var _);
            DecisionVariable variable = null;
            if (contain_variable)
            {
                variable = _parser.Parse(link);
            }

            return 
            (
                contain_variable,
                contain_variable ? variable.Name : "",
                contain_variable ? TypeHelper.GetTypeString(variable.Type) : "",
                contain_variable ? variable.Value.ToString() : ""
            );
        }
    }
}