namespace Tailwind.Css.TagHelpers;

using System;
using System.Text.Encodings.Web;

using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Options;

[HtmlTargetElement("*", Attributes = ForAttributeName + ", " + ErrorClassAttributeName)]
public class ValidationStatusTagHelper : TagHelper
{
    protected const string ForAttributeName = "asp-for";
    protected const string DefaultClassAttributeName = "default-class";
    protected const string ErrorClassAttributeName = "error-class";

    private readonly TagOptions _settings;

    public ValidationStatusTagHelper(IOptions<TagOptions> settings)
        => _settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));

    /// <summary>
    /// An expression to be evaluated against the current model.
    /// </summary>
    [HtmlAttributeName(ForAttributeName)]
    public ModelExpression? For { get; set; }

    /// <summary>
    /// The classes to apply when the form field doesn't have any errors.
    /// </summary>
    [HtmlAttributeName(DefaultClassAttributeName)]
    public string? DefaultClass { get; set; }

    /// <summary>
    /// The classes to apply when the form input has one or more errors.
    /// </summary>
    [HtmlAttributeName(ErrorClassAttributeName)]
    public string? ErrorClass { get; set; }

    [HtmlAttributeNotBound]
    [ViewContext]
    public ViewContext ViewContext { get; set; } = null!;

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (context is null) throw new ArgumentNullException(nameof(context));
        if (output is null) throw new ArgumentNullException(nameof(output));

        if (For is null)
        {
            return;
        }

        ViewContext.ModelState.TryGetValue(For.Name, out var entry);

        var classList = entry?.Errors.Count > 0
            ? Utilities.SplitClassList(ErrorClass)
            : Utilities.SplitClassList(DefaultClass);

        if (_settings.IncludeComments)
        {
            output.PreElement.AppendHtmlLine("<!--");

            output.PreElement.Append("  For: ");
            output.PreElement.AppendLine(For.Name);

            output.PreElement.Append("  Base: ");
            output.PreElement.AppendLine(Utilities.ExtractClassValue(output));

            output.PreElement.Append("  Default: ");
            output.PreElement.AppendLine(DefaultClass ?? "");

            output.PreElement.Append("  Error: ");
            output.PreElement.AppendLine(ErrorClass ?? "");

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
