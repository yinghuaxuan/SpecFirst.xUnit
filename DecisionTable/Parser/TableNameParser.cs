namespace SpecFirst.Core.DecisionTable.Parser
{
    using System.Linq;
    using System.Xml.Linq;

    public sealed class TableNameParser
    {
        public string Parse(XElement table)
        {
            var firstRow = table.Descendants("tr").First();
            var column = firstRow.Descendants("th").Union(firstRow.Descendants("td")).First();
            var splitValue = column.Value.Split(':');
            if (splitValue.Length > 1)
            {
                return splitValue[1].Trim();
            }
            return splitValue[0].Trim();
        }
    }
}