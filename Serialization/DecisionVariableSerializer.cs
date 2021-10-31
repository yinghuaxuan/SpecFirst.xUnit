using System.Diagnostics;

namespace SpecFirst.Core.Serialization
{
    public class DecisionVariableSerializer
    {
        public string Serialize(object data)
        {
            Debug.Assert(data is DecisionVariable.DecisionVariable);

            var variable = (DecisionVariable.DecisionVariable)data;
            return variable.Name;
        }
    }
}
