namespace SpecFirst.Core.DecisionTable.Validator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    public sealed class DecisionTableHtmlValidator : IDecisionTableHtmlValidator
    {
        public bool Validate(XElement document, out IEnumerable<string> errors)
        {
            errors = new List<string>();

            IEnumerable<XElement> rows = document.Descendants("tr").ToList();
            if (!ValidateRowNumbers(rows, (List<string>)errors)) goto end;
            if (!ValidateFirstRow(rows.First(), (List<string>)errors)) goto end;
            if (!ValidateSecondRow(rows.Skip(1).First(), (List<string>)errors)) goto end;

            end:
            return !errors.Any();
        }

        private static bool ValidateRowNumbers(IEnumerable<XElement> rows, List<string> errors)
        {
            if (rows.Count() < 3)
            {
                errors.Add("Decision table must have at least three rows");
                return false;
            }

            return true;
        }

        private static bool ValidateFirstRow(XElement firstRow, List<string> errors)
        {
            IEnumerable<XElement> columns = firstRow.Descendants("td").Union(firstRow.Descendants("th"));
            if (columns.Count() != 1)
            {
                errors.Add("The first row of the decision table must have a single column");
                return false;
            }

            if (string.IsNullOrWhiteSpace(columns.First().Value))
            {
                errors.Add("The first row of the decision table must contain some text");
                return false;
            }

            return true;
        }

        private static bool ValidateSecondRow(XElement secondRow, List<string> errors)
        {
            IEnumerable<XElement> columns = secondRow.Descendants("td").Union(secondRow.Descendants("th"));
            
            if (columns.Any(c => string.IsNullOrWhiteSpace(c.Value)))
            {
                errors.Add("The second row of the decision table must contain some text in each column");
                return false;
            }

            if (columns.All(c => c.Value.Trim().StartsWith("#")))
            {
                errors.Add("The second row of the decision table must contains at least one column that is not a comment column");
                return false;
            }

            return true;
        }
    }
}