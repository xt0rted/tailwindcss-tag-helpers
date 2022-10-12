namespace Tailwind.Css.TagHelpers;

using System.Text.Encodings.Web;

using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;

internal static class TagHelperAttributeListExtensions
{
    public static string GetValue(this TagHelperAttributeList attributes, string name)
    {
        if (!attributes.TryGetAttribute(name, out var classAttribute))
        {
            return "";
        }

        if (classAttribute?.Value is null)
        {
            return "";
        }

        var result = classAttribute.Value switch
        {
            string stringValue => stringValue,
            HtmlString htmlString => htmlString.Value,
            IHtmlContent htmlContent => ExtractHtmlContent(htmlContent),
            _ => classAttribute.Value.ToString(),
        };

        return result ?? "";
    }

    private static string ExtractHtmlContent(IHtmlContent htmlContent)
    {
        using var stringWriter = new StringWriter();

        htmlContent.WriteTo(stringWriter, HtmlEncoder.Default);

        return stringWriter.ToString();
    }
}
