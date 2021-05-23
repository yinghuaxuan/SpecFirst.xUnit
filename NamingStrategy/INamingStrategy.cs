namespace SpecFirst.Core.NamingStrategy
{
    public interface INamingStrategy
    {
        string Resolve(string raw);
    }
}