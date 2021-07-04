namespace SpecFirst.Core.DecisionTable.Parser
{
    using System;
    using System.Linq;
    using System.Xml.Linq;

    public sealed class TableTypeParser
    {
        public TableType Parse(XElement table)
        {
            var firstRow = table.Descendants("tr").First();
            var column = firstRow.Descendants("th").Union(firstRow.Descendants("td")).First();
            var splitValue = column.Value.Split(':');
            if (splitValue.Length > 1)
            {
                return (TableType)Enum.Parse(typeof(TableType), splitValue[0], true);
            }
            return TableType.Decision;
        }
    }
}