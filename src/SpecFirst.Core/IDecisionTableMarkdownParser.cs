namespace SpecFirst.Core
{
    using System.Collections.Generic;

    public interface IDecisionTableMarkdownParser
    {
        IEnumerable<DecisionTable.DecisionTable> Parse(
            string markdownText,
            out IEnumerable<DecisionTable.DecisionVariable> variables);
    }
}