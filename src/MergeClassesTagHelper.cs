namespace Tailwind.Css.TagHelpers;

using System.Text.Encodings.Web;

using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Options;

[HtmlTargetElement("*", Attributes = ForAttributeName)]
public class MergeDefaultClassTagHelper : TagHelper
{
    protected const string ForAttributeName = "merge-classes";
    protected const string DefaultClassAttributeName = "default-class";

    private readonly TagOptions _settings;

    public MergeDefaultClassTagHelper(IOptions<TagOptions> settings)
        => _settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));

    /// <summary>
    /// The classes to merge into the main class list.
    /// </summary>
    [HtmlAttributeName(DefaultClassAttributeName)]
    public string? DefaultClass { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        ArgumentNullException.ThrowIfNull(output);

        var classList = Utilities.SplitClassList(DefaultClass);

        if (_settings.IncludeComments)
        {
            output.PreElement.AppendHtmlLine("<!--");

            output.PreElement.Append("  Base: ");
            output.PreElement.AppendLine(output.Attributes.GetValue("class"));

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
