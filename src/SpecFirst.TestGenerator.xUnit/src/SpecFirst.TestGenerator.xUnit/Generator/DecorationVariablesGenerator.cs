namespace SpecFirst.TestGenerator.xUnit.Generator
{
    using System.Linq;
    using SpecFirst.Core.DecisionTable;

    public class DecorationVariablesGenerator
    {
        private readonly ITableHeaderToParameterConverter _parameterConverter;

        public DecorationVariablesGenerator(ITableHeaderToParameterConverter parameterConverter)
        {
            _parameterConverter = parameterConverter;
        }

        public dynamic Convert(DecisionTable table)
        {
            var parameters = table.TableHeaders.Select(h => _parameterConverter.Convert(h)).Where(p => !string.IsNullOrWhiteSpace(p.Decoration));
            var decorationVariables = parameters.Select(p => $"string {p.Name}_decoration = \"{p.Decoration}\"");
            
            return new
            {
                decoration_variables = decorationVariables
            };
        }
    }
}
