namespace SpecFirst.TestGenerator.xUnit.Generator
{
    using System;
    using System.Linq;
    using Core.Serialization;
    using HandlebarsDotNet;
    using SpecFirst.Core.DecisionVariable;
    using SpecFirst.TestGenerator.xUnit.Template;

    public class ClassFieldGenerator
    {
        private readonly IPrimitiveDataSerializer _primitiveDataSerializer;

        public ClassFieldGenerator(IPrimitiveDataSerializer primitiveDataSerializer)
        {
            _primitiveDataSerializer = primitiveDataSerializer;
        }

        public string Convert(DecisionVariable[] variables)
        {
            Func<object, string> compiled = Handlebars.Compile(XUnitTemplate.CLASS_VARIABLE_TEMPLATE);

            return compiled(new
            {
                class_variables = variables.Select(v => new
                {
                    VariableType = CSharpTypeAlias.Alias(v.Type),
                    VariableName = v.Name,
                    VariableValue = v.Value == null ? null : _primitiveDataSerializer.Serialize(v.Value)
                })
            });
        }
    }
}