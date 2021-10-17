namespace SpecFirst.Core.Serialization
{
    using System.Diagnostics;

    public class NullDataSerializer
    {
        public string Serialize(object data)
        {
            Debug.Assert(data == null);

            return $"{"null".ToLowerInvariant()}";
        }
    }
}
