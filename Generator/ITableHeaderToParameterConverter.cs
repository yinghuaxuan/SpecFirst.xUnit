namespace SpecFirst.TestGenerator.xUnit.Generator
{
    using SpecFirst.Core.DecisionTable;

    public interface ITableHeaderToParameterConverter
    {
        Parameter Convert(TableHeader tableHeader);
    }
}