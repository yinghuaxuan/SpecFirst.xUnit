using SpecFirst.Core.DecisionVariable;
using System.Collections.Generic;

namespace SpecFirst.TestGenerator.xUnit.Converter
{
    public class TableVariableToClassVariableConverter
    {
        public IEnumerable<string> Convert(DecisionVariable[] variables)
        {
            foreach (var variable in variables)
            {
                yield return $"private readonly {CSharpTypeAlias.Alias(variable.Type)} {variable.Name} = {variable.Value}";
            }
        }
    }
}
