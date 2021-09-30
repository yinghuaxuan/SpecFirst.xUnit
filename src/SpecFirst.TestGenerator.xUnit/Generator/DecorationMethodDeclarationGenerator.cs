namespace SpecFirst.TestGenerator.xUnit.Generator
{
    using System.Linq;
    using SpecFirst.Core.DecisionTable;

    public class DecorationMethodDeclarationGenerator
    {
        private readonly ITableHeaderToParameterConverter _parameterConverter;

        public DecorationMethodDeclarationGenerator(ITableHeaderToParameterConverter parameterConverter)
        {
            _parameterConverter = parameterConverter;
        }

        public dynamic Convert(string tableName, TableHeader[] tableHeaders)
        {
            var parameters = tableHeaders.SelectMany(h => _parameterConverter.Convert(h)).Where(p => p.Name.EndsWith("_decoration"));

            return new
            {
                decoration_methods = parameters.Select(p => new
                {
                    ReturnType = p.Type,
                    ParameterName = p.Name,
                    InputParameters = $"{p.Type} {p.Name.Replace("_decoration", "")}, string {p.Name}"
                })
            };
        }
    }
}
