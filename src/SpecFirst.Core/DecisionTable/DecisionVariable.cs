namespace SpecFirst.Core.DecisionTable
{
    using System;

    public class DecisionVariable
    {
        public string Name { get; set; }
        public Type Type { get; set; }
        public object Value { get; set; }
    }
}
