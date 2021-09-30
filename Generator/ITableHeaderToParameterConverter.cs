namespace SpecFirst.TestGenerator.xUnit.Generator
{
    using System.Collections.Generic;
    using SpecFirst.Core.DecisionTable;

    public interface ITableHeaderToParameterConverter
    {
        IEnumerable<Parameter> Convert(TableHeader tableHeader);
    }
}