namespace SpecFirst.TestGenerator.xUnit.Converter
{
    using SpecFirst.Core.DecisionTable;
    using SpecFirst.TestGenerator.xUnit.TestGeneration;

    public interface ITableHeaderToParameterConverter
    {
        Parameter Convert(TableHeader tableHeader);
    }
}