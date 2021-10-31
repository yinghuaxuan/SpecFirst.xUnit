namespace SpecFirst.Core.TypeResolver
{
    using System;

    public static class TypeResolver
    {
        public static Type? Resolve(string? value, out object? parsedValue)
        {
            parsedValue = value;
            if (value == null)
            {
                return null;
            }
            if (string.IsNullOrWhiteSpace(value))
            {
                parsedValue = value;
                return typeof(string);
            }

            value = value.Trim();
            Type type;
            if (value.StartsWith("["))
            {
                type = CollectionTypeResolver.Resolve(value, out var collectionValue);
                parsedValue = collectionValue;
            }
            else
            {
                type = PrimitiveTypeResolver.Resolve(value, out var scalaValue);
                parsedValue = scalaValue;
            }

            return type;
        }
    }
}
