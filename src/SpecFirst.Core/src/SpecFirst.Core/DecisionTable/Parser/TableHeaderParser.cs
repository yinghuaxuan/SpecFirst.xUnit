using System.Linq;
using System.Xml.Linq;

namespace SpecFirst.Core.DecisionTable.Parser
{
    using System;

    public sealed class TableHeaderParser
    {
        public TableHeader Parse(XElement header)
        {
            var headerName = ParseHeaderName(header, out var direction);
            var decoration = ParseHeaderDecoration(header);
            return new TableHeader(headerName, direction, decoration);
        }

        private static string ParseHeaderName(XElement header, out TableHeaderType direction)
        {
            ReadOnlySpan<char> headerSpan = header.Value.Trim().AsSpan();

            direction = TableHeaderType.Input;
            if (headerSpan.StartsWith("#".AsSpan()))
            {
                direction = TableHeaderType.Comment;
                headerSpan = headerSpan.Slice(1, headerSpan.Length - 1);
            }
            else if (headerSpan.EndsWith("?".AsSpan()))
            {
                direction = TableHeaderType.Output;
                headerSpan = headerSpan.Slice(0, headerSpan.Length - 1);
            }

            return headerSpan.ToString();
        }

        private static string ParseHeaderDecoration(XElement header)
        {
            var links = header.Descendants("a");
            if (links.Any())
            {
                var link = links.ElementAt(0);
                return link.Attribute("title")?.Value?.Trim();
            }

            return null;
        }
    }
}