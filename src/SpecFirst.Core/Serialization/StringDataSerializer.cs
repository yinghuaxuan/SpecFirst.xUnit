namespace SpecFirst.Core.Serialization
{
    using System;
    using Utils;

    public class StringDataSerializer
    {
        public string Serialize(object data)
        {
            string result;
            if (string.Equals(data.ToString(), "null", StringComparison.OrdinalIgnoreCase))
            {
                result = $"{data.ToString().ToLowerInvariant()}";
            }
            else
            {
                result = $"\"{SanitizeString(data.ToString())}\"";
            }

            return result;
        }

        private static string SanitizeString(string value)
        {
            if (value.StartsWith("\"") && value.EndsWith("\""))
            {
                value = value.TrimFirst("\"").TrimLast("\"");
            }

            return 
                value
                .Replace("\n", " ")
                .Replace("\r", " ")
                //.Replace(@"""", @"\""")
                .Replace("\\", "\\\\")
                .Replace("\"", "\\\"")
                ;
        }
    }
}
