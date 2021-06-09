namespace SpecFirst.Core.DecisionTable.Validator
{
    using System.Collections.Generic;
    using System.Xml.Linq;

    public interface IDecisionTableHtmlValidator
    {
        bool Validate(XElement document, out IEnumerable<string> errors);
    }
}