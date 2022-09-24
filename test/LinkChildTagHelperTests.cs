namespace Tailwind.Css.TagHelpers;

using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Options;

public class LinkChildTagHelperTests : TagHelperTestBase
{
    [Fact]
    public void Should_throw_when_no_link_context_set()
    {
        // Given
        var context = MakeTagHelperContext(
            tagName: "span",
            new TagHelperAttributeList
            {
                { "class", "text-black" },
            });
        var output = MakeTagHelperOutput(
            tagName: "span",
            new TagHelperAttributeList
            {
                { "class", "text-black" },
            });

        var options = Options.Create(new TagOptions());
        var helper = new LinkChildTagHelper(options)
        {
            CurrentClass = "bg-orange no-underline",
            DefaultClass = "bg-white underline",
            ViewContext = MakeViewContext(),
        };

        // When
        var result = () => helper.Process(context, output);

        // Then
        result.ShouldThrow<ParentContextNotFoundException>();
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void Should_do_nothing_if_options_are_not_set(bool isMatch)
    {
        // Given
        var context = MakeTagHelperContext(
            tagName: "span",
            new TagHelperAttributeList
            {
                { "class", "text-black" },
            });
        var output = MakeTagHelperOutput(
            tagName: "span",
            new TagHelperAttributeList
            {
                { "class", "text-black" },
            });

        AddLinkContext(context, isMatch);

        var options = Options.Create(new TagOptions());
        var helper = new LinkChildTagHelper(options)
        {
            ViewContext = MakeViewContext(),
        };

        // When
        helper.Process(context, output);

        // Then
        output.Attributes["class"].ShouldNotBeNull();

        var classList = output.Attributes["class"].Value as string;
        classList.ShouldNotBeNull();
        classList.ShouldBe("text-black");
    }

    [Fact]
    public void Should_add_default_classes_when_not_the_current_url()
    {
        // Given
        var context = MakeTagHelperContext(
            tagName: "span",
            new TagHelperAttributeList
            {
                { "class", "text-black" },
            });
        var output = MakeTagHelperOutput(
            tagName: "span",
            new TagHelperAttributeList
            {
                { "class", "text-black" },
            });

        AddLinkContext(context, isMatch: false);

        var options = Options.Create(new TagOptions());
        var helper = new LinkChildTagHelper(options)
        {
            CurrentClass = "bg-orange no-underline",
            DefaultClass = "bg-white underline",
            ViewContext = MakeViewContext(),
        };

        // When
        helper.Process(context, output);

        // Then
        output.Attributes["class"].ShouldNotBeNull();

        var classList = output.Attributes["class"].Value as HtmlString;
        classList.ShouldNotBeNull();
        classList.Value.ShouldBe("text-black bg-white underline");
    }

    [Fact]
    public void Should_add_current_classes_when_a_match()
    {
        // Given
        var context = MakeTagHelperContext(
            tagName: "span",
            new TagHelperAttributeList
            {
                { "class", "text-black" },
            });
        var output = MakeTagHelperOutput(
            tagName: "span",
            new TagHelperAttributeList
            {
                { "class", "text-black" },
            });

        AddLinkContext(context, isMatch: true);

        var options = Options.Create(new TagOptions());
        var helper = new LinkChildTagHelper(options)
        {
            CurrentClass = "bg-orange no-underline",
            DefaultClass = "bg-white underline",
            ViewContext = MakeViewContext(),
        };

        // When
        helper.Process(context, output);

        // Then
        output.Attributes["class"].ShouldNotBeNull();

        var classList = output.Attributes["class"].Value as HtmlString;
        classList.ShouldNotBeNull();
        classList.Value.ShouldBe("text-black bg-orange no-underline");
    }

    [Fact]
    public void Should_not_emit_comments_when_setting_is_disabled()
    {
        // Given
        var context = MakeTagHelperContext(
            tagName: "span",
            new TagHelperAttributeList
            {
                { "class", "text-black" },
            });
        var output = MakeTagHelperOutput(
            tagName: "span",
            new TagHelperAttributeList
            {
                { "class", "text-black" },
                { "href", "/foo" },
            });

        AddLinkContext(context, isMatch: true);

        var options = Options.Create(
            new TagOptions
            {
                IncludeComments = false,
            });
        var helper = new LinkChildTagHelper(options)
        {
            CurrentClass = "bg-orange no-underline",
            DefaultClass = "bg-white underline",
            ViewContext = MakeViewContext(),
        };

        // When
        helper.Process(context, output);

        // Then
        output.PreElement.GetContent().ShouldBeEmpty();
    }

    [Fact]
    public void Should_emit_comments_when_setting_is_enabled()
    {
        // Given
        var context = MakeTagHelperContext(
            tagName: "span",
            new TagHelperAttributeList
            {
                { "class", "text-black" },
            });
        var output = MakeTagHelperOutput(
            tagName: "span",
            new TagHelperAttributeList
            {
                { "class", "text-black" },
                { "href", "/foo" },
            });

        AddLinkContext(context, isMatch: true);

        var options = Options.Create(
            new TagOptions
            {
                IncludeComments = true,
            });
        var helper = new LinkChildTagHelper(options)
        {
            CurrentClass = "bg-orange no-underline",
            DefaultClass = "bg-white underline",
            ViewContext = MakeViewContext(),
        };

        // When
        helper.Process(context, output);

        // Then
        output.PreElement.GetContent().ShouldBe(
            """
            <!--
              Base: text-black
              Current: bg-orange no-underline
              Default: bg-white underline
            -->

            """);
    }

    private static void AddLinkContext(TagHelperContext context, bool isMatch)
        => context.Items.Add(
            typeof(LinkContext),
            new LinkContext
            {
                IsMatch = isMatch,
                MatchStyle = PathMatchStyle.Full,
            });
}
