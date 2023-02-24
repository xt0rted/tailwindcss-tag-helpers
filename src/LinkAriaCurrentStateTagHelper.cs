namespace Tailwind.Css.TagHelpers;

using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

[HtmlTargetElement("a", Attributes = AriaCurrentMatchAttributeName)]
public class LinkAriaCurrentStateTagHelper : TagHelper
{
    private const string AriaCurrentMatchAttributeName = "aria-current-state";

    private readonly ILogger<LinkAriaCurrentStateTagHelper> _logger;
    private readonly TagOptions _settings;

    public LinkAriaCurrentStateTagHelper(
        ILogger<LinkAriaCurrentStateTagHelper> logger,
        IOptions<TagOptions> settings)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));
    }

    // Puts us after the LinkTagHelper
    public override int Order => 1002;

    [HtmlAttributeName(AriaCurrentMatchAttributeName)]
    public LinkAriaCurrentState State { get; set; } = LinkAriaCurrentState.Page;

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (context is null) throw new ArgumentNullException(nameof(context));
        if (output is null) throw new ArgumentNullException(nameof(output));

        if (!context.Items.TryGetValue(typeof(LinkContext), out var ctx) || ctx is not LinkContext linkContext)
        {
            var message = $"Parent context not found for element using {AriaCurrentMatchAttributeName}=\"{State}\" tag helper";

            _logger.LogError(message);

            if (_settings.IncludeComments)
            {
                output.PreElement.AppendHtml("<!-- ⚠️ ");
                output.PreElement.AppendHtml(message);
                output.PreElement.AppendHtmlLine(" -->");
            }

            return;
        }

        if (linkContext.IsMatch)
        {
            output.Attributes.Add("aria-current", StateValue(State));
        }
    }

    private static string StateValue(LinkAriaCurrentState value)
        => value switch
        {
            LinkAriaCurrentState.True => "true",
            LinkAriaCurrentState.Page => "page",
            LinkAriaCurrentState.Step => "step",
            LinkAriaCurrentState.False => "false",
            LinkAriaCurrentState.Location => "location",
            LinkAriaCurrentState.Date => "date",
            LinkAriaCurrentState.Time => "time",
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, "Unsupported aria-current state"),
        };
}
