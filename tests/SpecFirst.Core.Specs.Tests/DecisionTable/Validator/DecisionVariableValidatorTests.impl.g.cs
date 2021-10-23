
namespace SpecFirst.Core.Specs.Tests
{
    using System;
    using System.Xml.Linq;
    using SpecFirst.Core.DecisionVariable;
    using SpecFirst.Core.DecisionVariable.Parser;
    using SpecFirst.Core.DecisionVariable.Validator;

    public partial class validate_decision_variables_from_links
    {
        DecisionVariableValidator _validator;
        DecisionVariableParser _parser;

        public validate_decision_variables_from_links()
        {
            _validator = new DecisionVariableValidator();
            _parser = new DecisionVariableParser();
        }

        private partial (bool, string, string, string) validate_decision_variables_from_links_implementation(string text)
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
                contain_variable ? variable.Name : null,
                contain_variable ? TypeHelper.GetTypeString(variable.Type) : null,
                contain_variable ? variable.Value.ToString() : null
            );
        }
    }
    
}