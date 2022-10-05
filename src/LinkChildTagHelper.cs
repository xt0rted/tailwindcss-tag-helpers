namespace Tailwind.Css.TagHelpers;

using System.Text.Encodings.Web;

using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Options;

[HtmlTargetElement(ParentTag = "a", Attributes = CurrentClassAttributeName)]
[HtmlTargetElement(ParentTag = "a", Attributes = DefaultClassAttributeName)]
public class LinkChildTagHelper : LinkTagHelperBase
{
    public LinkChildTagHelper(
        IOptions<TagOptions> settings,
        HtmlEncoder htmlEncoder)
        : base(settings, htmlEncoder)
    {
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (context is null) throw new ArgumentNullException(nameof(context));
        if (output is null) throw new ArgumentNullException(nameof(output));

        if (!context.Items.TryGetValue(typeof(LinkContext), out var contextItem) || contextItem is not LinkContext linkContext)
        {
            throw new ParentContextNotFoundException();
        }

        MergeClassLists(output, linkContext.IsMatch);
    }
}
