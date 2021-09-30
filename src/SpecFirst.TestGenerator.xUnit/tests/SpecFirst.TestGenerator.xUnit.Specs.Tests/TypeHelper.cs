using System;
using System.Collections.Generic;

namespace SpecFirst.TestGenerator.xUnit.Specs.Tests
{
    public static class TypeHelper
    {
        private static readonly Dictionary<string, Type> CSharpTypeAlias = new Dictionary<string, Type>
        {
            {"object", typeof(object)
            },
            {"string", typeof(string)
            },
            { "int", typeof(int)},
            { "double", typeof(double)},
            { "decimal", typeof(decimal)},
            { "bool", typeof(bool)},
            { "DateTime", typeof(DateTime)},
        };

        public static Type GetTypeFromString(string type)
        {
            return CSharpTypeAlias[type];
        }
    }
}
