namespace SpecFirst.Core.Setting
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;

    public class SpecFirstSettingManager
    {
        public const string DefaultSpecFileExtension = ".spec.md";
        public const string DefaultTestingFramework = "xUnit";
        public const string DefaultTestProjectName = "{spec_project_name}.Tests";
        public const bool DefaultUseSpecProject = false;
        public const string DefaultTestFilePath = "{spec_file_path}";
        public const string DefaultTestFileName = "{spec_name}Tests.g.cs";
        public const string DefaultImplFilePath = "{spec_file_path}";
        public const string DefaultImplFileName = "{spec_name}Tests.impl.g.cs";

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
        }

        public SpecFirstSettings Settings { get; }

        public SpecFirstSettings GetSettings(string specFile)
        {
            return new SpecFirstSettings
            {
                SpecFileExtension = Settings.SpecFileExtension,
                TestingFramework = Settings.TestingFramework,
                TestProject = new TestProject
                {
                    UseSpecProject = Settings.TestProject.UseSpecProject,
                    TestProjectName = GetTestProject(),
                    TestFilePath = Settings.TestProject.TestFilePath.Replace("{spec_file_path}", GetSpecFilePath(specFile)),
                    TestFileName = GetTestFileName(specFile),
                    ImplFilePath = Settings.TestProject.ImplFilePath.Replace("{spec_file_path}", GetSpecFilePath(specFile)),
                    ImplFileName = GetTestImplFileName(specFile),
                    TestNameSpace = GetTestNamespace(specFile)
                }
            };
        }

        private string GetSpecName(string specFile)
        {
            return $"{new FileInfo(specFile).Name.Replace(Settings.SpecFileExtension, "")}";
        }

        private string GetSpecFilePath(string specFile)
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

        private string GetTestNamespace(string specFile)
        {
            string specPath = Path.GetDirectoryName(specFile)!;
            string[] paths = specPath.Split(
                new[] { _specProject },
                StringSplitOptions.RemoveEmptyEntries);

            if (paths.Length == 1) // spec file is at the root of the project
            {
                return $"{GetTestProject()}.{GetSpecName(specFile)}";
            }
            if (paths.Length == 2)
            {
                return $"{GetTestProject()}.{paths[1].Trim('\\').Replace('\\', '.')}.{GetSpecName(specFile)}";
            }

            throw new InvalidOperationException($"The path {specFile} is not valid");
        }

        private string GetTestFileName(string specFile)
        {
            return Settings.TestProject.TestFileName.Replace("{spec_name}", GetSpecName(specFile));
        }

        private string GetTestImplFileName(string specFile)
        {
            return Settings.TestProject.ImplFileName.Replace("{spec_name}", GetSpecName(specFile));
        }

        private string GetTestProject()
        {
            return Settings.TestProject.TestProjectName!.Replace("{spec_project_name}", _specProject);
        }

        private SpecFirstSettings Parse(string settingFile)
        {
            XDocument settings = XDocument.Load(settingFile);
            return new SpecFirstSettings
            {
                TestingFramework = settings.Descendants("TestingFramework").FirstOrDefault()?.Value ?? DefaultTestingFramework,
                SpecFileExtension = settings.Descendants("SpecFileExtension").FirstOrDefault()?.Value ?? DefaultSpecFileExtension,
                TestProject = new TestProject
                {
                    UseSpecProject = bool.TryParse(settings.Descendants("UseSpecProject").FirstOrDefault()?.Value, out var result) ? result : DefaultUseSpecProject,
                    TestProjectName = settings.Descendants("TestProjectName").FirstOrDefault()?.Value ?? DefaultTestProjectName,
                    TestFilePath = settings.Descendants("TestFilePath").FirstOrDefault()?.Value ?? DefaultTestFilePath,
                    TestFileName = settings.Descendants("TestFileName").FirstOrDefault()?.Value ?? DefaultTestFileName,
                    ImplFilePath = settings.Descendants("ImplFilePath").FirstOrDefault()?.Value ?? DefaultImplFilePath,
                    ImplFileName = settings.Descendants("ImplFileName").FirstOrDefault()?.Value ?? DefaultImplFileName,
                }
            };
        }

        private SpecFirstSettings Default()
        {
            return new SpecFirstSettings
            {
                TestingFramework = DefaultTestingFramework,
                SpecFileExtension = DefaultSpecFileExtension,
                TestProject = new TestProject
                {
                    UseSpecProject = DefaultUseSpecProject,
                    TestProjectName = DefaultTestProjectName,
                    TestFilePath = DefaultTestFilePath,
                    TestFileName = DefaultTestFileName,
                    ImplFilePath = DefaultImplFilePath,
                    ImplFileName = DefaultImplFileName,
                }
            };
        }
    }
}
