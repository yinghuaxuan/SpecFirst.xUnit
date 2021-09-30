namespace SpecFirst.TestGenerator.xUnit.Generator
{
    using System.Linq;
    using Core.Serialization;
    using SpecFirst.Core.DecisionVariable;

    public class ClassFieldsGenerator
    {
        private readonly IPrimitiveDataSerializer _primitiveDataSerializer;

        public ClassFieldsGenerator(IPrimitiveDataSerializer primitiveDataSerializer)
        {
            _primitiveDataSerializer = primitiveDataSerializer;
        }

        public dynamic Convert(DecisionVariable[] variables)
        {
            return new
            {
                class_variables = variables.Select(v => new
                {
                    VariableType = CSharpTypeAlias.Alias(v.Type),
                    VariableName = v.Name,
                    VariableValue = v.Value == null ? null : _primitiveDataSerializer.Serialize(v.Value)
                })
            };
        }
    }
}