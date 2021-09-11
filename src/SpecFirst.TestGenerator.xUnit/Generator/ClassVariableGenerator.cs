namespace SpecFirst.TestGenerator.xUnit.Generator
{
    using System;
    using System.Linq;
    using HandlebarsDotNet;
    using SpecFirst.Core.DecisionVariable;
    using SpecFirst.TestGenerator.xUnit.Template;

    public class ClassVariableGenerator
    {
        public string Convert(DecisionVariable[] variables)
        {
            Func<object, string> compiled = Handlebars.Compile(XUnitTemplate.CLASS_VARIABLE_TEMPLATE);

            return compiled(new
            {
                class_variables = variables.Select(v => new
                    { VariableType = v.Type, VariableName = v.Name, VariableValue = v.Value })
            });
        }
    }
}