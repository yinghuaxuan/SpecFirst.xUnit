
namespace SpecFirst.MarkdownParser
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Xml.Linq;
    using Jurassic;
    using SpecFirst.Core;
    using SpecFirst.Core.DecisionTable;
    using SpecFirst.Core.DecisionTable.Parser;
    using SpecFirst.Core.DecisionTable.Validator;
    using SpecFirst.Core.DecisionVariable;
    using SpecFirst.Core.DecisionVariable.Parser;
    using SpecFirst.Core.DecisionVariable.Validator;

    public sealed class DecisionTableMarkdownParser : IDecisionTableMarkdownParser
    {
        private readonly DecisionVariableParser _variableParser;
        private readonly DecisionVariableValidator _variableValidator;
        private readonly IDecisionTableParser _tableParser;
        private readonly IDecisionTableHtmlValidator _tableValidator;

        public DecisionTableMarkdownParser()
        {
            _variableParser = new DecisionVariableParser();
            _variableValidator = new DecisionVariableValidator();
            _tableParser = new DecisionTableParser();
            _tableValidator = new DecisionTableHtmlValidator();
        }

        public IEnumerable<DecisionTable> Parse(string markdownText)
        {
            string html = ParseMarkdownToHtml(markdownText);
            html = html.Replace("<br>", "<br/>");
            XDocument document = ParseHtmlToXml(html);
            var variables = ParseDeisionVariables(document);
            List<DecisionTable> decisionTables = ParseDecisionTables(document, variables);
            return decisionTables;
        }

        private List<DecisionVariable> ParseDeisionVariables(XDocument document)
        {
            List<DecisionVariable> decisionVariables = new List<DecisionVariable>();
            IEnumerable<XElement> links = document.Descendants("a");
            foreach (XElement link in links)
            {
                if (_variableValidator.Validate(link, out _))
                {
                    DecisionVariable decisionVariable = _variableParser.Parse(link);
                    decisionVariables.Add(decisionVariable);
                }
            }

            return decisionVariables;
        }

        private List<DecisionTable> ParseDecisionTables(XDocument document, IEnumerable<DecisionVariable> variables)
        {
            List<DecisionTable> decisionTables = new List<DecisionTable>();
            IEnumerable<XElement> tables = document.Descendants("table");
            foreach (XElement table in tables)
            {
                if (_tableValidator.Validate(table, out _))
                {
                    DecisionTable decisionTable = _tableParser.Parse(table, variables);
                    decisionTables.Add(decisionTable);
                }
            }

            return decisionTables;
        }

        private static XDocument ParseHtmlToXml(string html)
        {
            XDocument document;
            try
            {
                document = XDocument.Parse("<html>" + html + "</html>");
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"Can not parse the generated html into xml", e);
            }

            return document;
        }

        private static string ParseMarkdownToHtml(string markdownText)
        {
            string html;
            try
            {
                var engine = new ScriptEngine();
                engine.SetGlobalValue("markdownTable", markdownText);
                engine.Execute(GetScript());
                html = engine.GetGlobalValue("result").ToString();
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"Can not parse markdown text to HTML", e);
            }

            return html;
        }

        private static string GetScript()
        {
            var assembly = Assembly.GetExecutingAssembly();
            string resourcePath = $"{assembly.GetName().Name}.Script.bundle.js";
            using Stream stream = assembly.GetManifestResourceStream(resourcePath)!;
            using StreamReader reader = new StreamReader(stream!);
            var script = reader.ReadToEnd();
            return script;
        }
    }
}
