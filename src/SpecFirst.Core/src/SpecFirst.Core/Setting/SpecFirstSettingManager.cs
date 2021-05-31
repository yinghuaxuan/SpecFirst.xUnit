namespace SpecFirst.Core.Setting
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;

    public class SpecFirstSettingManager
    {
        public const string DefaultTestingFramework = "xUnit";
        public const string DefaultTestProject = "{spec_project}.Tests";
        public const string DefaultTestFileName = "{spec_name}Tests.g.cs";
        public const string DefaultImplementationFileName = "{spec_name}Tests.impl.g.cs";
        public const string DefaultSpecFileExtension = ".spec.md";

        private readonly string? _specProject;

        public SpecFirstSettingManager(string? settingFile, string? specProject)
        {
            _specProject = specProject;
            if (settingFile != null)
            {
                Settings = Parse(settingFile);
            }
            else
            {
                Settings = Default();
            }

            Settings.TestGeneration.TestProject = GetTestProject();
        }

        public SpecFirstSettings Settings { get; }

        public string GetTestProject()
        {
            return Settings.TestGeneration.TestProject!.Replace("{spec_project}", _specProject);
        }

        public string GetSpecName(string specFile)
        {
            return $"{new FileInfo(specFile).Name.Replace(Settings.SpecFileExtension, "")}";
        }

        public string GetTestFileName(string specFile)
        {
            return Settings.TestGeneration.TestFileName.Replace("{spec_name}", GetSpecName(specFile));
        }

        public string GetTestImplFileName(string specFile)
        {
            return Settings.TestGeneration.TestImplFileName.Replace("{spec_name}", GetSpecName(specFile));
        }

        public string GetTestFilePath(string specFile)
        {
            string specPath = Path.GetDirectoryName(specFile)!;
            string[] paths = specPath.Split(
                new[] { _specProject },
                StringSplitOptions.RemoveEmptyEntries);
            
            if (paths.Length == 1) // spec file is at the root of the project
            {
                return Path.Combine(paths[0], GetTestProject());
            }
            if (paths.Length == 2)
            {
                return Path.Combine(paths[0], GetTestProject(), paths[1].TrimStart('\\'));
            }

            return specPath;
        }

        private SpecFirstSettings Parse(string settingFile)
        {
            XDocument settings = XDocument.Load(settingFile);
            return new SpecFirstSettings
            {
                TestingFramework = settings.Descendants("TestingFramework").FirstOrDefault()?.Value ?? DefaultTestingFramework,
                SpecFileExtension = settings.Descendants("SpecFileExtension").FirstOrDefault()?.Value ?? DefaultSpecFileExtension,
                TestGeneration = new TestGeneration
                {
                    TestProject = settings.Descendants("TestProject").FirstOrDefault()?.Value ?? DefaultTestFileName,
                    TestFileName = settings.Descendants("TestFileName").FirstOrDefault()?.Value ?? DefaultTestFileName,
                    TestImplFileName = settings.Descendants("ImplFileName").FirstOrDefault()?.Value ?? DefaultImplementationFileName,
                }
            };
        }

        private SpecFirstSettings Default()
        {
            return new SpecFirstSettings
            {
                TestingFramework = DefaultTestingFramework,
                SpecFileExtension = DefaultSpecFileExtension,
                TestGeneration = new TestGeneration
                {
                    TestProject = DefaultTestProject,
                    TestFileName = DefaultTestFileName,
                    TestImplFileName = DefaultImplementationFileName,
                }
            };
        }
    }
}
