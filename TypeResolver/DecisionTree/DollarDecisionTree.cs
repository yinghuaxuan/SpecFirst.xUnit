namespace SpecFirst.Core.TypeResolver
{
    public static class DollarDecisionTree
    {
        public static TypeDecisionNode ConstructDollarTree()
        {
            var dollarNode = DollarNode();
            var anyNode = AnyNode();

            var letterNode = LetterNode();
            dollarNode.ChildNodes.Add(letterNode);
            dollarNode.ChildNodes.Add(anyNode);

            var anyAfterLetterNode = AnyAfterLetterNode();
            letterNode.ChildNodes.Add(anyAfterLetterNode);

            return dollarNode;
        }
        
        private static TypeDecisionNode DollarNode()
        {
            return new TypeDecisionNode
            {
                ShouldProcess = c => c == '$',
                NodeType = (s) =>
                {
                    return new TypeValuePair(typeof(string), s);
                }
            };
        }

        private static TypeDecisionNode LetterNode()
        {
            return new TypeDecisionNode
            {
                ShouldProcess = c => char.IsLetter(c),
                NodeType = (s) =>
                {
                    return new TypeValuePair(
                        typeof(DecisionVariable.DecisionVariable),
                        new DecisionVariable.DecisionVariable(s.TrimStart('$'), null, null));
                }
            };
        }

        private static TypeDecisionNode AnyAfterLetterNode()
        {
            return new TypeDecisionNode
            {
                ShouldProcess = c => true,
                NodeType = (s) =>
                {
                    return new TypeValuePair(
                        typeof(DecisionVariable.DecisionVariable),
                        new DecisionVariable.DecisionVariable(s.TrimStart('$'), null, null));
                }
            };
        }

        private static TypeDecisionNode AnyNode()
        {
            return new TypeDecisionNode
            {
                ShouldProcess = c => true,
                NodeType = (s) => new TypeValuePair(typeof(string), s)
            };
        }
    }
}
