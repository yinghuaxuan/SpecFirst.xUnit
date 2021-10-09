using System;

namespace SpecFirst.Core.Utils
{
    public static class TypeExtensions
    {
        public static Type ConvertToNullable(this Type type)
        {
            // If the given type is already nullable, just return it
            if (type.IsNullable())
                return type;

            // Use Nullable.GetUnderlyingType() to remove the Nullable<T> wrapper if type is already nullable.
            type = Nullable.GetUnderlyingType(type) ?? type; // avoid type becoming null
            return type.IsValueType ? typeof(Nullable<>).MakeGenericType(type) : type;
        }

        public static bool IsNullable(this Type type)
        {
            // If this is not a value type, it is a reference type, so it is automatically nullable
            //  (NOTE: All forms of Nullable<T> are value types)
            if (!type.IsValueType)
                return true;

            // Report whether an underlying Type exists (if it does, type is a nullable Type)
            return Nullable.GetUnderlyingType(type) != null;
        }

        public static Type GetUnderlyingType(this Type type)
        {
            // Report an underlying Type if exists (if it does, type is a nullable Type)
            return Nullable.GetUnderlyingType(type) ?? type;
        }
    }
}
