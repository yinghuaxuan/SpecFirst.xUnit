namespace SpecFirst.Core.Setting
{
    public class SpecFirstSettings
    {
        public string SpecFileExtension { get; set; }
        public string TestingFramework { get; set; }
        public TestProject TestProject { get; set; }
    }

    public class TestProject
    {
        public bool UseSpecProject { get; set; }
        public string TestProjectName { get; set; }
        public string TestFilePath { get; set; }
        public string TestFileName { get; set; }
        public string ImplFilePath { get; set; }
        public string ImplFileName { get; set; }

        // synthetic
        public string TestNameSpace { get; set; }
    }
}