using System;
using System.Diagnostics;
using System.Linq;
using SpecFirst.Core.TypeResolver;

namespace SpecFirst.Core.Serialization
{
    public class ArrayDataSerializer : IArrayDataSerializer
    {
        private readonly ISingularDataSerializer _singularDataSerializer;

        public ArrayDataSerializer(ISingularDataSerializer singularDataSerializer)
        {
            _singularDataSerializer = singularDataSerializer;
        }

        public string Serialize(object data, Type targetType)
        {
            Debug.Assert(data.GetType().IsArray);

            return SerializeArray(data, targetType);
        }

        private string SerializeArray(object data, Type targetType)
        {
            string serialized;
            if (targetType == typeof(IntType[]))
            {
                serialized = $"new int[] {{{string.Join(", ", (data as object[]).Select(_singularDataSerializer.Serialize))}}}";
            }
            else if (targetType == typeof(DecimalType[]))
            {
                serialized = $"new decimal[] {{{string.Join(", ", (data as object[]).Select(_singularDataSerializer.Serialize))}}}";
            }
            else if (targetType == typeof(DoubleType[]))
            {
                serialized = $"new double[] {{{string.Join(", ", (data as object[]).Select(_singularDataSerializer.Serialize))}}}";
            }
            else if (targetType == typeof(bool[]))
            {
                serialized = $"new bool[] {{{string.Join(", ", (data as object[]).Select(_singularDataSerializer.Serialize))}}}";
            }
            else if (targetType == typeof(DateTime[]))
            {
                serialized = $"new DateTime[] {{{string.Join(", ", (data as object[]).Select(_singularDataSerializer.Serialize))}}}";
            }
            else if (targetType == typeof(string[]))
            {
                serialized = $"new string[] {{{string.Join(", ", (data as object[]).Select(_singularDataSerializer.Serialize))}}}";
            }
            else if (targetType == typeof(object[]))
            {
                serialized = $"new object[] {{{string.Join(", ", (data as object[]).Select(_singularDataSerializer.Serialize))}}}";
            }
            else
            {
                throw new InvalidOperationException($"The type {data.GetType().Name} is not supported for array serialization");
            }

            return serialized;
        }
    }
}
