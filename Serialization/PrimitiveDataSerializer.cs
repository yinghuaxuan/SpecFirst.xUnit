namespace SpecFirst.Core.Serialization
{
    using System;
    using TypeResolver;

    public class PrimitiveDataSerializer : IPrimitiveDataSerializer
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
            string data;
            switch (value)
            {
                case IntType _:
                case DoubleType _:
                case DecimalType _:
                    data = _numberSerializer.Serialize(value);
                    break;
                case DateTime _:
                    data = _datetimeSerializer.Serialize(value);
                    break;
                case bool _:
                    data = _booleanSerializer.Serialize(value);
                    break;
                case string _:
                    data = _stringSerializer.Serialize(value);
                    break;
                default:
                    throw new InvalidOperationException();
            }

            return data;
        }
    }
}