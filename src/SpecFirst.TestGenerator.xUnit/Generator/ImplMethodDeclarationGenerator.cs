namespace SpecFirst.TestGenerator.xUnit.Generator
{
    using System.Linq;
    using SpecFirst.Core.DecisionTable;

    public class ImplMethodDeclarationGenerator
    {
        private readonly ITableHeaderToParameterConverter _parameterConverter;
        private readonly ITableNameToClassNameConverter _classNameConverter;

        public ImplMethodDeclarationGenerator(ITableHeaderToParameterConverter parameterConverter, ITableNameToClassNameConverter classNameConverter)
        {
            _parameterConverter = parameterConverter;
            _classNameConverter = classNameConverter;
        }

        public dynamic Convert(string tableName, TableHeader[] tableHeaders)
        {
            var parameters = tableHeaders.SelectMany(h => _parameterConverter.Convert(h)).Where(p => !p.Name.EndsWith("_decoration"));
            var inputParameterString = string.Join(", ", parameters.Where(p => p.Direction == ParameterDirection.Input).Select(p => p));
            var outputTypes = parameters.Where(p => p.Direction == ParameterDirection.Output).Select(p => p.Type);
            string outputTypeString = "void";
            if (outputTypes.Count() == 1)
            {
                outputTypeString = outputTypes.ElementAt(0);
            }
            else if (outputTypes.Count() > 1)
            {
                outputTypeString = new string(outputTypeString.Prepend('(').Append(')').ToArray());
            }

            return new
            {
                class_name = _classNameConverter.Convert(tableName),
                impl_return_types = outputTypeString,
                impl_input_parameters = inputParameterString
            };
        }
    }
}
