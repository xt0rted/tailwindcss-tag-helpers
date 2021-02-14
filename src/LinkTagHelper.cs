namespace Tailwind.Css.TagHelpers
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Text.Encodings.Web;

    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.TagHelpers;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Razor.TagHelpers;

    [HtmlTargetElement("a")]
    public class LinkTagHelper : TagHelper
    {
        private static readonly char[] SpaceChars = { '\u0020', '\u0009', '\u000A', '\u000C', '\u000D' };

        // Puts us after the built-in link tag helper that resolves urls
        public override int Order => 1001;

        [HtmlAttributeName("current-class")]
        public string? CurrentClass { get; set; }

        [HtmlAttributeName("default-class")]
        public string? DefaultClass { get; set; }

        [HtmlAttributeNotBound]
        [ViewContext]
        [NotNull]
        public ViewContext? ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context is null) throw new ArgumentNullException(nameof(context));
            if (output is null) throw new ArgumentNullException(nameof(output));

            var target = ViewContext.HttpContext.Request.Path;

            var href = output.Attributes["href"]?.Value as string;

            string[]? classList;
            if (string.Equals(href, target, StringComparison.OrdinalIgnoreCase))
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
    }
}
