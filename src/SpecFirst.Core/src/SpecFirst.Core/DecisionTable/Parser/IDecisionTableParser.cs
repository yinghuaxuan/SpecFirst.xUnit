﻿namespace SpecFirst.Core.DecisionTable.Parser
{
    using System.Collections.Generic;
    using System.Xml.Linq;

    public interface IDecisionTableParser
    {
        DecisionTable Parse(XElement table, IList<DecisionVariable.DecisionVariable> decisionVariables);
    }
}