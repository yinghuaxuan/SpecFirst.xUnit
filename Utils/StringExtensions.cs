namespace SpecFirst.Core.Utils
{
    public static class StringExtensions
    {
        public static string Normalize(this string str, StringProcessingOptions options)
        {
            bool ignoreLineEnding = false;
            bool ignoreAllSpaces = false;
            if (options.HasFlag(StringProcessingOptions.IgnoreLineEnding))
            {
                ignoreLineEnding = true;
            }
            if (options.HasFlag(StringProcessingOptions.IgnoreAllSpaces))
            {
                ignoreAllSpaces = true;
            }
            if (ignoreLineEnding || ignoreAllSpaces)
            {
                str = TrimAllSpaces(str);
            }

            if (options.HasFlag(StringProcessingOptions.IgnoreCase))
            {
                str = str.ToUpperInvariant();
            }

            return str;
        }

        public static string TrimAllSpaces(this string str)
        {
            var len = str.Length;
            var src = str.ToCharArray();
            int dstIdx = 0;

            for (int i = 0; i < len; i++)
            {
                var c = src[i];
                switch (c)
                {
                    case '\u0020':
                    case '\u00A0':
                    case '\u1680':
                    case '\u2000':
                    case '\u2001':

                    case '\u2002':
                    case '\u2003':
                    case '\u2004':
                    case '\u2005':
                    case '\u2006':

                    case '\u2007':
                    case '\u2008':
                    case '\u2009':
                    case '\u200A':
                    case '\u202F':

                    case '\u205F':
                    case '\u3000':
                    case '\u2028':
                    case '\u2029':
                    case '\u0009':

                    case '\u000A':
                    case '\u000B':
                    case '\u000C':
                    case '\u000D':
                    case '\u0085':
                        continue;

                    default:
                        src[dstIdx++] = c;
                        break;
                }
            }

            return new string(src, 0, dstIdx);
        }
    }
}
