namespace Tailwind.Css.TagHelpers;

using System;

internal static class Utilities
{
    private static readonly char[] SpaceChars =
    {
        '\u0020', // Space
        '\u0009', // Tab
        '\u000A', // Line Feed
        '\u000C', // Form Feed
        '\u000D', // Carriage Return
    };

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
