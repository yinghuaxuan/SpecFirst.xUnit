namespace SpecFirst.TestGenerator.xUnit.Generator
{
    public class Parameter
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public ParameterDirection Direction { get; set; }
        public override string ToString()
        {
            return $"{Type} {Name}";
        }
    }
}