using System.Xml.Linq;

namespace SpecFirst.Core.DecisionVariable.Parser
{
    public class DecisionVariableParser
    {
        public DecisionVariable Parse(XElement link)
        {
            string variableName = link.Attribute("title").Value.TrimStart('$');
            string variableValue = link.Value;
            return new DecisionVariable(variableName, typeof(string), variableValue);
        }
    }
}
