namespace SpecFirst.Core.DecisionVariable
{
    using SpecFirst.Core.DecisionTable;
    using System;
    using System.Collections.Generic;

    public class DecisionVariable
    {
        public DecisionVariable(string name, Type? type, object? value)
        {
            Name = name;
            Type = type;
            Value = value;
            AssociatedTableHeader = new HashSet<TableHeader>();
        }

        public string Name { get; }
        public Type? Type { get; set; }
        public object? Value { get; set; }
        public HashSet<TableHeader> AssociatedTableHeader { get; }

        public override bool Equals(object? obj)
        {
            return obj is DecisionVariable variable &&
                   Name == variable.Name;
        }

        public override int GetHashCode()
        {
            return 539060726 + EqualityComparer<string>.Default.GetHashCode(Name);
        }
    }
}
