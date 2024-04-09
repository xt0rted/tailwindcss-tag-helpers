namespace Tailwind.Css.TagHelpers;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Options;

[HtmlTargetElement("a", Attributes = CurrentClassAttributeName)]
[HtmlTargetElement("a", Attributes = DefaultClassAttributeName)]
public class LinkTagHelper : LinkTagHelperBase
{
    public LinkTagHelper(IOptions<TagOptions> settings)
        : base(settings)
    {
    }

    // Puts us after the built-in link tag helper that resolves urls
    public override int Order => 1001;

    [HtmlAttributeName("match")]
    public PathMatchStyle MatchStyle { get; set; } = PathMatchStyle.Full;

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(output);

        var currentPath = ViewContext.HttpContext.Request.Path;
        var linkPath = new PathString(output.Attributes.GetValue("href"));
        var isMatch = IsMatch(currentPath, linkPath);

        context.Items.Add(
            typeof(LinkContext),
            new LinkContext
            {
                IsMatch = isMatch,
                MatchStyle = MatchStyle,
            });

        MergeClassLists(output, isMatch);
    }

    private bool IsMatch(PathString currentPath, PathString linkPath)
        => MatchStyle switch
        {
            PathMatchStyle.Base => currentPath.StartsWithSegments(linkPath, StringComparison.OrdinalIgnoreCase),
            _ => currentPath.Equals(linkPath, StringComparison.OrdinalIgnoreCase),
        };
}
