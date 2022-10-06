namespace Tailwind.Css.TagHelpers;

using System.Text.Encodings.Web;

using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Options;

public abstract class LinkTagHelperBase : TagHelper
{
    protected const string CurrentClassAttributeName = "current-class";
    protected const string DefaultClassAttributeName = "default-class";

    private readonly TagOptions _settings;

    protected LinkTagHelperBase(IOptions<TagOptions> settings)
        => _settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));

    /// <summary>
    /// The classes to apply when the link url matches the current url.
    /// </summary>
    [HtmlAttributeName(CurrentClassAttributeName)]
    public string? CurrentClass { get; set; }

    /// <summary>
    /// The classes to apply when the link url doesn't match the current url.
    /// </summary>
    [HtmlAttributeName(DefaultClassAttributeName)]
    public string? DefaultClass { get; set; }

    [HtmlAttributeNotBound]
    [ViewContext]
    public ViewContext ViewContext { get; set; } = null!;

    protected void MergeClassLists(TagHelperOutput output, bool isMatch)
    {
        if (output is null) throw new ArgumentNullException(nameof(output));

        var classList = isMatch
            ? Utilities.SplitClassList(CurrentClass)
            : Utilities.SplitClassList(DefaultClass);

        if (_settings.IncludeComments)
        {
            output.PreElement.AppendHtmlLine("<!--");

            output.PreElement.Append("  Base: ");
            output.PreElement.AppendLine(Utilities.ExtractClassValue(output));

            output.PreElement.Append("  Current: ");
            output.PreElement.AppendLine(CurrentClass ?? "");

            output.PreElement.Append("  Default: ");
            output.PreElement.AppendLine(DefaultClass ?? "");

            output.PreElement.AppendHtmlLine("-->");
        }

        if (classList?.Length > 0)
        {
            foreach (var className in classList)
            {
                output.AddClass(className, HtmlEncoder.Default);
            }
        }
    }
}
