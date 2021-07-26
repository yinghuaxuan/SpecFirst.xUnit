namespace SpecFirst.Core.DecisionTable.Parser
{
    using System;

    public sealed class TableHeaderParser
    {
        public TableHeader Parse(string header)
        {
            ReadOnlySpan<char> headerSpan = header.AsSpan();

            TableHeaderType direction;
            if (headerSpan.StartsWith("#".AsSpan()))
            {
                headerSpan = headerSpan.Slice(1, headerSpan.Length - 1);
                direction = TableHeaderType.Comment;
            }
            else if (headerSpan.EndsWith("?".AsSpan()))
            {
                headerSpan = headerSpan.Slice(0, headerSpan.Length - 1);
                direction = TableHeaderType.Output;
            }
            else
            {
                direction = TableHeaderType.Input;
            }

            return new TableHeader(headerSpan.ToString(), direction);
        }
    }
}