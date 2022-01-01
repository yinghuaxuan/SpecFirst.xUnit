namespace SpecFirst.Core.DecisionTable
{
    using SpecFirst.Core.DecisionVariable;

    public class DecisionTable
    {
        public DecisionTable(
            TableType tableType,
            string tableName,
            TableHeader[] tableHeaders,
            object?[,] tableData,
            DecisionVariable[]? decisionVariables = null)
        {
            TableType = tableType;
            TableName = tableName;
            TableHeaders = tableHeaders;
            TableData = tableData;
            DecisionVariables = decisionVariables;
            PopulateParent();
        }

        public TableType TableType { get; }
        public string TableName { get; }
        public TableHeader[] TableHeaders { get; }
        public object?[,] TableData { get; }
        public DecisionVariable[]? DecisionVariables { get; }

        private void PopulateParent()
        {
            foreach (var tableHeader in TableHeaders)
            {
                tableHeader.SetParent(this);
            }
        }
    }
}