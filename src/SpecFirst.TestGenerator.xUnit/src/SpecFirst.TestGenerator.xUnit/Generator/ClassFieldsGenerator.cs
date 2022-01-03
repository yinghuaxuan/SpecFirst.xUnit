namespace SpecFirst.TestGenerator.xUnit.Generator
{
    using System.Linq;
    using Core.Serialization;
    using SpecFirst.Core.DecisionTable;
    using SpecFirst.Core.DecisionVariable;

    public class ClassFieldsGenerator
    {
        private readonly ISingularDataSerializer _singularDataSerializer;

        public ClassFieldsGenerator(ISingularDataSerializer singularDataSerializer)
        {
            _singularDataSerializer = singularDataSerializer;
        }

        public dynamic Convert(DecisionTable table)
        {
            return new
            {
                class_variables = table.DecisionVariables.Select(v => new
                {
                    VariableType = CSharpTypeAlias.Alias(v.Type),
                    VariableName = v.Name,
                    VariableValue = v.Value == null ? null : _singularDataSerializer.Serialize(v.Value)
                })
            };
        }
    }
}