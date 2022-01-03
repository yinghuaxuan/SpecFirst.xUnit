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

        public dynamic Convert(DecisionTable table)
        {
            var parameters = table.TableHeaders.Select(h => _parameterConverter.Convert(h)).Where(p => !string.IsNullOrWhiteSpace(p.Decoration));

            return new
            {
                decoration_methods = parameters.Select(p => new
                {
                    ReturnType = p.Type,
                    ParameterName = $"{ p.Name}_decoration",
                    InputParameters = $"{p.Type} {p.Name}, string {p.Name}_decoration"
                })
            };
        }
    }
}
