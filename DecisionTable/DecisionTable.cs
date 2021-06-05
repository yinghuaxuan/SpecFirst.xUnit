namespace SpecFirst.Core.DecisionTable
{
    public sealed class DecisionTable
    {
        public DecisionTable(
            string fixtureName,
            TableHeader[] tableHeaders,
            object[,] tableData,
            DecisionVariable.DecisionVariable[] decisionVariables = null)
        {
            TableName = fixtureName;
            TableHeaders = tableHeaders;
            TableData = tableData;
            DecisionVariables = decisionVariables;
        }

        public string TableName { get; }
        public TableHeader[] TableHeaders { get; }
        public object[,] TableData { get; }
        public DecisionVariable.DecisionVariable[] DecisionVariables { get; }
    }
}