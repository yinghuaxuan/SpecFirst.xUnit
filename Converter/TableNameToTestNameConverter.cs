using SpecFirst.Core.NamingStrategy;

namespace SpecFirst.TestGenerator.xUnit.Converter
{
    public class TableNameToTestNameConverter
    {
        private readonly INamingStrategy _namingStrategy;

        public TableNameToTestNameConverter(INamingStrategy namingStrategy)
        {
            _namingStrategy = namingStrategy;
        }

        public string Convert(string tableName)
        {
            return _namingStrategy.Resolve(tableName);
        }
    }
}
