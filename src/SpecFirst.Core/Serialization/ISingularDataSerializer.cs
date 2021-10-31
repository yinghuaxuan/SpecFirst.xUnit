namespace SpecFirst.Core.Serialization
{
    public interface ISingularDataSerializer
    {
        string Serialize(object? data);
    }
}