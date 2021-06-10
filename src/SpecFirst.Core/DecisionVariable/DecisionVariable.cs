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
            AssociatedTableHeaders = new HashSet<TableHeader>();
        }

        public string Name { get; }
        public Type? Type { get; set; }
        public object? Value { get; set; }
        public HashSet<TableHeader> AssociatedTableHeaders { get; }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((DecisionVariable)obj);
        }

        public static bool operator ==(DecisionVariable? left, DecisionVariable? right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(DecisionVariable? left, DecisionVariable? right)
        {
            return !Equals(left, right);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        protected bool Equals(DecisionVariable other)
        {
            return Name == other.Name;
        }

    }
}