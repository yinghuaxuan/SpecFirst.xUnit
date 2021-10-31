namespace SpecFirst.Core.Serialization
{
    using System;
    using TypeResolver;

    public class PrimitiveDataSerializer
    {
        private readonly StringDataSerializer _stringSerializer;
        private readonly NumberDataSerializer _numberSerializer;
        private readonly DateTimeDataSerializer _datetimeSerializer;
        private readonly BooleanDataSerializer _booleanSerializer;

        public PrimitiveDataSerializer()
        {
            _stringSerializer = new StringDataSerializer();
            _numberSerializer = new NumberDataSerializer();
            _datetimeSerializer = new DateTimeDataSerializer();
            _booleanSerializer = new BooleanDataSerializer();
        }

        public string Serialize(object? value)
        {
            string data = value switch
            {
                IntType _ => _numberSerializer.Serialize(value),
                DoubleType _ => _numberSerializer.Serialize(value),
                DecimalType _ => _numberSerializer.Serialize(value),
                DateTime _ => _datetimeSerializer.Serialize(value),
                bool _ => _booleanSerializer.Serialize(value),
                string _ => _stringSerializer.Serialize(value),
                _ => throw new InvalidOperationException($"The type {value.GetType().Name} is not supported for SingularDataSerializer")
            };

            return data;
        }
    }
}