namespace SpecFirst.Core.NamingStrategy
{
    public class SnakeCaseNamingStrategy : INamingStrategy
    {
        public string Resolve(string raw)
        {
            return raw.ToLowerInvariant().Replace(" ", "_");
        }
    }
}
