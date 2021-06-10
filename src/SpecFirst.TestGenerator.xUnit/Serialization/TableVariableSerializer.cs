namespace SpecFirst.TestGenerator.xUnit.Serialization
{
    using SpecFirst.Core.DecisionVariable;
    using System.Diagnostics;

    public class TableVariableSerializer : ITableVariableSerializer
    {
        public string Serialize(object data)
        {
            Debug.Assert(data is DecisionVariable);

            var variable = (DecisionVariable)data;
            return variable.Name;
        }
    }
}
