namespace SpecFirst.TestGenerator.xUnit.Generator
{
    using System.Text.RegularExpressions;
    using SpecFirst.Core.NamingStrategy;

    public class TableNameToClassNameConverter : ITableNameToClassNameConverter
    {
        private readonly INamingStrategy _namingStrategy;

        public TableNameToClassNameConverter(INamingStrategy namingStrategy)
        {
            _namingStrategy = namingStrategy;
        }

        public string Convert(string tableName)
        {
            var sanitizedName = ReplaceIllegalCharacters(tableName);
            return _namingStrategy.Resolve(sanitizedName);
        }

        private string ReplaceIllegalCharacters(string input)
        {
            return Regex.Replace(input, @"[^\w@-]", "_");
        }
    }
}
