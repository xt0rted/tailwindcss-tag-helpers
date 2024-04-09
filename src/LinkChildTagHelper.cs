namespace Tailwind.Css.TagHelpers;

using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Options;

[HtmlTargetElement(ParentTag = "a", Attributes = CurrentClassAttributeName)]
[HtmlTargetElement(ParentTag = "a", Attributes = DefaultClassAttributeName)]
public class LinkChildTagHelper : LinkTagHelperBase
{
    public LinkChildTagHelper(IOptions<TagOptions> settings)
        : base(settings)
    {
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(output);

        if (!context.Items.TryGetValue(typeof(LinkContext), out var contextItem) || contextItem is not LinkContext linkContext)
        {
            throw new ParentContextNotFoundException();
        }

        MergeClassLists(output, linkContext.IsMatch);
    }
}
