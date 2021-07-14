namespace SpecFirst.Core.DecisionTable
{
    using SpecFirst.Core.DecisionVariable;

    public sealed class DecisionTable
    {
        public DecisionTable(
            TableType tableType,
            string tableName,
            TableHeader[] tableHeaders,
            object[,] tableData,
            DecisionVariable[]? decisionVariables = null)
        {
            TableType = tableType;
            TableName = tableName;
            TableHeaders = tableHeaders;
            TableData = tableData;
            DecisionVariables = decisionVariables;
        }

        public TableType TableType { get; }
        public string TableName { get; }
        public TableHeader[] TableHeaders { get; }
        public object[,] TableData { get; }
        public DecisionVariable[]? DecisionVariables { get; }
    }
}