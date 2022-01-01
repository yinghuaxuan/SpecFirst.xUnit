namespace SpecFirst
{
    using System;
    using Microsoft.CodeAnalysis;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;
    using SpecFirst.Core;
    using SpecFirst.Core.DecisionTable;
    using SpecFirst.Core.Setting;
    using SpecFirst.MarkdownParser;
    using SpecFirst.TestGenerator.xUnit;

    [Generator]
    public sealed class XUnitGenerator : ISourceGenerator
    {
        private static readonly DiagnosticDescriptor UnableParseMarkdownText = new(
            id: "SF002",
            title: "Couldn't parse markdown text",
            messageFormat: "Couldn't process the markdown file {0} due to error {1}",
            category: "MarkdownParser",
            DiagnosticSeverity.Error,
            isEnabledByDefault: true);
        private static readonly DiagnosticDescriptor UnableGenerateTests = new(
            id: "SF003",
            title: "Couldn't generate tests",
            messageFormat: "Couldn't generate tests for the markdown file {0} due to error {1}",
            category: "MarkdownParser",
            DiagnosticSeverity.Error,
            isEnabledByDefault: true);
        private static readonly DiagnosticDescriptor UnexpectedError = new(
            id: "SF001",
            title: "Unexpected error",
            messageFormat: "Unexpected error when running SpecFirst.xUnit source generator - {0}",
            category: "SpecFirst.xUnit",
            DiagnosticSeverity.Error,
            isEnabledByDefault: true);
        private static readonly DiagnosticDescriptor TestsGenerated = new(
            id: "SF006",
            title: "Tests Generated",
            messageFormat: "Generated test file {0} from markdown file {1}",
            category: "SpecFirst.xUnit",
            DiagnosticSeverity.Info,
            isEnabledByDefault: true);

        private IDecisionTableMarkdownParser _markdownParser;
        private ITestsGenerator _testsGenerator;
        private SpecFirstSettingManager _settingManager;

        public void Initialize(GeneratorInitializationContext context)
        {
            // Debugger.Launch();
        }

        public void Execute(GeneratorExecutionContext context)
        {
            try
            {
                AdditionalText settingFile =
                    context
                        .AdditionalFiles
                        .FirstOrDefault(f => f.Path.EndsWith("specfirst.config", StringComparison.OrdinalIgnoreCase));
                _settingManager = new SpecFirstSettingManager(settingFile?.Path, context.Compilation?.AssemblyName);
                _markdownParser = new DecisionTableMarkdownParser();
                _testsGenerator = new XUnitTestsGenerator()!;

                IEnumerable<AdditionalText> markdownFiles =
                    context.AdditionalFiles.Where(at => at.Path.EndsWith(_settingManager.Settings.SpecFileExtension));
                foreach (AdditionalText file in markdownFiles)
                {
                    var settings = _settingManager.GetSettings(file.Path);
                    ProcessMarkdownFile(settings, file, context);
                }
            }
            catch (Exception e)
            {
                context.ReportDiagnostic(Diagnostic.Create(UnexpectedError, Location.None, e.ToString()));
            }
        }

        private void ProcessMarkdownFile(
            SpecFirstSettings settings,
            AdditionalText markdownFile,
            GeneratorExecutionContext context)
        {
            if (!TryParseMarkdownFile(context, markdownFile, out List<DecisionTable> tables))
            {
                return;
            }

            if (!TryGenerateTests(context, settings, markdownFile, tables, out List<string> sources))
            {
                return;
            }

            PersistTestFiles(markdownFile, sources, settings, context);
        }

        private bool TryParseMarkdownFile(
            GeneratorExecutionContext context,
            AdditionalText markdownFile,
            out List<DecisionTable> tables)
        {
            tables = new List<DecisionTable>();
            try
            {
                var markdownText = markdownFile.GetText(context.CancellationToken)?.ToString();
                tables.AddRange(_markdownParser.Parse(markdownText!));
                return true;
            }
            catch (Exception e)
            {
                context.ReportDiagnostic(Diagnostic.Create(UnableParseMarkdownText, Location.None, markdownFile.Path, e.ToString()));
            }

            return false;
        }

        private bool TryGenerateTests(
            GeneratorExecutionContext context,
            SpecFirstSettings settings,
            AdditionalText markdownFile,
            List<DecisionTable> tables,
            out List<string> sources)
        {
            sources = new List<string>();
            try
            {
                var tablesToGenerate = tables.Where(t => t.TableType != TableType.Comment);
                sources.AddRange(_testsGenerator.Generate(settings, tablesToGenerate));
                return true;
            }
            catch (Exception e)
            {
                context.ReportDiagnostic(Diagnostic.Create(UnableGenerateTests, Location.None, markdownFile.Path, e.ToString()));
            }

            return false;
        }

        private void PersistTestFiles(
            AdditionalText markdownFile,
            IEnumerable<string> sources,
            SpecFirstSettings settings,
            GeneratorExecutionContext context)
        {
            var filePath = settings.TestProject.TestFilePath;

            Directory.CreateDirectory(filePath); // create the directory in case it doesn't exist

            PersistTestFile(markdownFile, settings, sources.ElementAt(0), context);

            PersistTestImplFile(markdownFile, settings, sources.ElementAt(1), context);
        }

        private void PersistTestFile(
            AdditionalText markdownFile,
            SpecFirstSettings settings,
            string tests,
            GeneratorExecutionContext context)
        {
            //context.AddSource($"{testFileName}", SourceText.From(tests, Encoding.UTF8));
            var testFilePath = settings.TestProject.TestFilePath;
            Directory.CreateDirectory(testFilePath); // create the directory in case it doesn't exist
            var testFileName = settings.TestProject.TestFileName;
            var testFile = Path.Combine(testFilePath, testFileName);
            File.WriteAllText(testFile, tests, Encoding.UTF8);

            context.ReportDiagnostic(Diagnostic.Create(TestsGenerated, Location.None, testFile, markdownFile.Path));
        }

        private void PersistTestImplFile(
            AdditionalText markdownFile,
            SpecFirstSettings settings,
            string implementations,
            GeneratorExecutionContext context)
        {
            var implFilePath = settings.TestProject.ImplFilePath;
            Directory.CreateDirectory(implFilePath); // create the directory in case it doesn't exist
            string implFileName = settings.TestProject.ImplFileName;
            var implFile = Path.Combine(implFilePath, implFileName);
            if (!File.Exists(implFile))
            {
                //context.AddSource($"{implFileName}", SourceText.From(implementations, Encoding.UTF8));
                File.WriteAllText(implFile, implementations, Encoding.UTF8);
            }
        }
    }
}
