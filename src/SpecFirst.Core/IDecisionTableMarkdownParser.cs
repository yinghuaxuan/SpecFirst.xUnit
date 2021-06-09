namespace SpecFirst.Core
{
    using System.Collections.Generic;

    public interface IDecisionTableMarkdownParser
    {
        IEnumerable<DecisionTable.DecisionTable> Parse(
            string markdownText,
            out IEnumerable<DecisionVariable.DecisionVariable> variables);
    }
}