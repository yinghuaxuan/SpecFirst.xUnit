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

        public dynamic Convert(DecisionTable table)
        {
            var parameters = table.TableHeaders.Select(h => _parameterConverter.Convert(h));
            var inputParameterString = string.Join(", ", parameters.Where(p => p.Direction == ParameterDirection.Input).Select(p => p));
            var outputTypes = parameters.Where(p => p.Direction == ParameterDirection.Output).Select(p => p.Type);
            string outputTypeString = "void";
            if (outputTypes.Count() == 1)
            {
                outputTypeString = outputTypes.ElementAt(0);
            }
            else if (outputTypes.Count() > 1)
            {
                outputTypeString = string.Join(", ", outputTypes);
                outputTypeString = new string(outputTypeString.Prepend('(').Append(')').ToArray());
            }

            return new
            {
                class_name = _classNameConverter.Convert(table.TableName),
                impl_return_types = outputTypeString,
                impl_input_parameters = inputParameterString
            };
        }
    }
}
