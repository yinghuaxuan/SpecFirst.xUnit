using System.Diagnostics;

namespace SpecFirst.Core.Serialization
{
    public class BooleanDataSerializer
    {
        public string Serialize(object data)
        {
            Debug.Assert(data is bool);

            return data.ToString().ToLowerInvariant();
        }
    }
}
