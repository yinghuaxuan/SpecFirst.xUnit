namespace SpecFirst.Core.Serialization
{
    using DecisionVariable;

    public class SingularDataSerializer : ISingularDataSerializer
    {
        private readonly DecisionVariableSerializer _decisionVariableSerializer;
        private readonly NullDataSerializer _nullDataSerializer;
        private readonly PrimitiveDataSerializer _primitiveDataSerializer;

        public SingularDataSerializer()
        {
            _decisionVariableSerializer = new DecisionVariableSerializer();
            _nullDataSerializer = new NullDataSerializer();
            _primitiveDataSerializer = new PrimitiveDataSerializer();
        }

        public string Serialize(object? data)
        {
            var value = data switch
            {
                null => _nullDataSerializer.Serialize(data),
                DecisionVariable => _decisionVariableSerializer.Serialize(data),
                _ => _primitiveDataSerializer.Serialize(data)
            };

            return value;
        }
    }
}