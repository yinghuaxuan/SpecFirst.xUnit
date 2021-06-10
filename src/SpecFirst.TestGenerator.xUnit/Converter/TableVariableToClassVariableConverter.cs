namespace SpecFirst.TestGenerator.xUnit.Converter
{
    using System.Diagnostics;
    using SpecFirst.TestGenerator.xUnit.Serialization;
    using SpecFirst.Core.DecisionVariable;
    using System.Collections.Generic;

    public class TableVariableToClassVariableConverter
    {
        public IEnumerable<string> Convert(DecisionVariable[] variables)
        {
            foreach (var variable in variables)
            {
                Debug.Assert(variable.Type == typeof(string));

                yield return $"private static readonly {CSharpTypeAlias.Alias(variable.Type)} {variable.Name} = {variable.Value.ToString().ToLiteral()}";
            }
        }
    }
}