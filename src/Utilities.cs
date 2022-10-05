namespace Tailwind.Css.TagHelpers;

using System;

internal static class Utilities
{
    private static readonly char[] SpaceChars = { '\u0020', '\u0009', '\u000A', '\u000C', '\u000D' };

    public static string[]? SplitClassList(string? classes)
    {
        if (string.IsNullOrWhiteSpace(classes))
        {
            return null;
        }

        return classes.Split(
            SpaceChars,
            StringSplitOptions.RemoveEmptyEntries);
    }
}
