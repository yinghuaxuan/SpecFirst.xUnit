namespace SpecFirst.Core.DecisionTable
{
    using System;

    public class TableHeader
    {
        private Type _dataType = typeof(object);
        private DecisionTable? _table;

        public TableHeader(string name, TableHeaderType type, string? decoration = null)
        {
            Name = name;
            TableHeaderType = type;
            Decoration = decoration;
        }

        public string Name { get; }
        public TableHeaderType TableHeaderType { get; }
        public string? Decoration { get; }
        public Type DataType => _dataType;
        public DecisionTable DecisionTable => _table!;

        public void UpdateDataType(Type type)
        {
            _dataType = type;
        }

        public void SetParent(DecisionTable table)
        {
            _table = table;
        }
    }
}