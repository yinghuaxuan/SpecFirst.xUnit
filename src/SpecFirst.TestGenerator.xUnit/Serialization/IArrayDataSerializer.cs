namespace SpecFirst.TestGenerator.xUnit.Serialization
{
    using System;

    public interface IArrayDataSerializer
    {
        string Serialize(object data, Type targetType);
    }
}