namespace Tailwind.Css.TagHelpers;

using System.Diagnostics.CodeAnalysis;
using System.Text.Encodings.Web;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

[HtmlTargetElement("a", Attributes = CurrentClassAttributeName)]
[HtmlTargetElement("a", Attributes = DefaultClassAttributeName)]
public class LinkTagHelper : TagHelper
{
    private const string CurrentClassAttributeName = "current-class";
    private const string DefaultClassAttributeName = "default-class";

    private static readonly char[] SpaceChars = { '\u0020', '\u0009', '\u000A', '\u000C', '\u000D' };

    // Puts us after the built-in link tag helper that resolves urls
    public override int Order => 1001;

    [HtmlAttributeName(CurrentClassAttributeName)]
    public string? CurrentClass { get; set; }

    [HtmlAttributeName(DefaultClassAttributeName)]
    public string? DefaultClass { get; set; }

    [HtmlAttributeName("match")]
    public PathMatchStyle MatchStyle { get; set; } = PathMatchStyle.Full;

    [HtmlAttributeNotBound]
    [ViewContext]
    [NotNull]
    public ViewContext? ViewContext { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (context is null) throw new ArgumentNullException(nameof(context));
        if (output is null) throw new ArgumentNullException(nameof(output));

        var currentPath = ViewContext.HttpContext.Request.Path;

        var linkPath = new PathString(output.Attributes["href"]?.Value as string);

        string[]? classList;
        if (IsMatch(currentPath, linkPath))
        {
            classList = CurrentClass?.Split(SpaceChars, StringSplitOptions.RemoveEmptyEntries);
        }
        else
        {
            classList = DefaultClass?.Split(SpaceChars, StringSplitOptions.RemoveEmptyEntries);
        }

        if (classList?.Length > 0)
        {
            foreach (var className in classList)
            {
                output.AddClass(className, HtmlEncoder.Default);
            }
        }
    }

    private bool IsMatch(PathString currentPath, PathString linkPath) =>
        MatchStyle switch
        {
            PathMatchStyle.Base => currentPath.StartsWithSegments(linkPath, StringComparison.OrdinalIgnoreCase),
            _ => currentPath.Equals(linkPath, StringComparison.OrdinalIgnoreCase),
        };
}
