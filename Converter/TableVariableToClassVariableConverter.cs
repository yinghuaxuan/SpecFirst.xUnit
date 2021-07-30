namespace SpecFirst.TestGenerator.xUnit.Converter
{
    using SpecFirst.TestGenerator.xUnit.Serialization;
    using SpecFirst.Core.DecisionVariable;
    using System.Collections.Generic;

    public class TableVariableToClassVariableConverter
    {
        public IEnumerable<string> Convert(DecisionVariable[] variables)
        {
            foreach (var variable in variables)
            {
                if (variable.Value != null)
                {
                    yield return $"private static readonly {CSharpTypeAlias.Alias(variable.Type)} {variable.Name} = {variable.Value.ToString().ToLiteral()}";
                }
                else
                {
                    yield return $"private static readonly {CSharpTypeAlias.Alias(variable.Type)} {variable.Name}";
                }
            }
        }
    }
}