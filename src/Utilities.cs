namespace Tailwind.Css.TagHelpers;

using System;
using System.Text.Encodings.Web;

using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;

internal static class Utilities
{
    private static readonly char[] SpaceChars = { '\u0020', '\u0009', '\u000A', '\u000C', '\u000D' };

    public static string ExtractClassValue(TagHelperOutput output, HtmlEncoder encoder)
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
            IHtmlContent htmlContent => ExtractHtmlContent(encoder, htmlContent),
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

    private static string ExtractHtmlContent(HtmlEncoder encoder, IHtmlContent htmlContent)
    {
        using var stringWriter = new StringWriter();

        htmlContent.WriteTo(stringWriter, encoder);

        return stringWriter.ToString();
    }
}
