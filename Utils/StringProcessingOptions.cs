using System;

namespace SpecFirst.Core.Utils
{
    [Flags]
    public enum StringProcessingOptions
    {
        IgnoreCase = 1,
        IgnoreLineEnding = 2,
        IgnoreAllSpaces = 4,
    }
}