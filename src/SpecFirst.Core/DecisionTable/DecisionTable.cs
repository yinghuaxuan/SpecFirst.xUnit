namespace SpecFirst.Core.DecisionTable
{
    using SpecFirst.Core.DecisionVariable;

    public sealed class DecisionTable
    {
        public DecisionTable(
            string fixtureName,
            TableHeader[] tableHeaders,
            object[,] tableData,
            DecisionVariable[]? decisionVariables = null)
        {
            TableName = fixtureName;
            TableHeaders = tableHeaders;
            TableData = tableData;
            DecisionVariables = decisionVariables;
        }

        public string TableName { get; }
        public TableHeader[] TableHeaders { get; }
        public object[,] TableData { get; }
        public DecisionVariable[]? DecisionVariables { get; }
    }
}