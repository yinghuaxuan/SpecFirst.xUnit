using System;

namespace SpecFirst.Core.Serialization
{
    public interface IArrayDataSerializer
    {
        string Serialize(object data, Type targetType);
    }
}