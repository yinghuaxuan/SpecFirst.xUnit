namespace SpecFirst.Core.DecisionTable
{
    using System;

    public class TableHeader
    {
        private Type _dataType = typeof(string);

        public TableHeader(string name, TableHeaderType type, string? additionalInfo = null)
        {
            Name = name;
            TableHeaderType = type;
            AdditionalInfo = additionalInfo;
        }

        public string Name { get; }
        public TableHeaderType TableHeaderType { get; }
        public string? AdditionalInfo { get; }
        public Type DataType => _dataType;

        public void UpdateDataType(Type type)
        {
            _dataType = type;
        }
    }
}