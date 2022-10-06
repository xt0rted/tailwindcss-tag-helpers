namespace Tailwind.Css.TagHelpers;

using System;
using System.Text.Encodings.Web;

using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;

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

    public static string ExtractClassValue(TagHelperOutput output)
    {
        if (!output.Attributes.TryGetAttribute("class", out var classAttribute))
        {
            return "";
        }

        if (classAttribute?.Value is null)
        {
            return "";
        }

        return classAttribute.Value switch
        {
            string stringValue => stringValue,
            HtmlString htmlString => htmlString.Value,
            IHtmlContent htmlContent => ExtractHtmlContent(htmlContent),
            _ => classAttribute.Value.ToString() ?? "",
        };
    }

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

    private static string ExtractHtmlContent(IHtmlContent htmlContent)
    {
        using var stringWriter = new StringWriter();

        htmlContent.WriteTo(stringWriter, HtmlEncoder.Default);

        return stringWriter.ToString();
    }
}
