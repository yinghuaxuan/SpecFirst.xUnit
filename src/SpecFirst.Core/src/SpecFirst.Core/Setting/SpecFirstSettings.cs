namespace SpecFirst.Core.Setting
{
    public class SpecFirstSettings
    {
        public string SpecFileExtension { get; set; }
        public string TestingFramework { get; set; }
        public TestGeneration TestGeneration { get; set; }
    }

    public class TestGeneration
    {
        public string TestProject { get; set; }
        public string TestFileName { get; set; }
        public string TestImplFileName { get; set; }
    }
}