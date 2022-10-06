namespace Tailwind.Css.TagHelpers;

using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Options;

public class LinkTagHelperTests : TagHelperTestBase
{
    [Fact]
    public void Should_do_nothing_if_options_are_not_set()
    {
        // Given
        var context = MakeTagHelperContext(
            tagName: "a",
            new TagHelperAttributeList
            {
                { "class", "text-black" },
            });
        var output = MakeTagHelperOutput(
            tagName: "a",
            new TagHelperAttributeList
            {
                { "class", "text-black" },
                { "href", "/foo" },
            });

        var options = Options.Create(new TagOptions());
        var helper = new LinkTagHelper(options)
        {
            ViewContext = MakeViewContext("/foo"),
        };

        // When
        helper.Process(context, output);

        // Then
        output.Attributes["class"].ShouldNotBeNull();

        var classList = output.Attributes["class"].Value as string;
        classList.ShouldNotBeNull();
        classList.ShouldBe("text-black");
    }

    [Theory]
    [InlineData(PathMatchStyle.Base)]
    [InlineData(PathMatchStyle.Full)]
    public void Should_add_default_classes_when_not_the_current_url(PathMatchStyle matchStyle)
    {
        // Given
        var context = MakeTagHelperContext(
            tagName: "a",
            new TagHelperAttributeList
            {
                { "class", "text-black" },
            });
        var output = MakeTagHelperOutput(
            tagName: "a",
            new TagHelperAttributeList
            {
                { "class", "text-black" },
                { "href", "/foo" },
            });

        var options = Options.Create(new TagOptions());
        var helper = new LinkTagHelper(options)
        {
            CurrentClass = "bg-orange no-underline",
            DefaultClass = "bg-white underline",
            MatchStyle = matchStyle,
            ViewContext = MakeViewContext("/bar"),
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
    public void Should_add_default_classes_when_base_path_does_not_match()
    {
        // Given
        var context = MakeTagHelperContext(
            tagName: "a",
            new TagHelperAttributeList
            {
                { "class", "text-black" },
            });
        var output = MakeTagHelperOutput(
            tagName: "a",
            new TagHelperAttributeList
            {
                { "class", "text-black" },
                { "href", "/foo" },
            });

        var options = Options.Create(new TagOptions());
        var helper = new LinkTagHelper(options)
        {
            CurrentClass = "bg-orange no-underline",
            DefaultClass = "bg-white underline",
            MatchStyle = PathMatchStyle.Base,
            ViewContext = MakeViewContext("/foobar"),
        };

        // When
        helper.Process(context, output);

        // Then
        output.Attributes["class"].ShouldNotBeNull();

        var classList = output.Attributes["class"].Value as HtmlString;
        classList.ShouldNotBeNull();
        classList.Value.ShouldBe("text-black bg-white underline");
    }

    [Theory]
    [InlineData("/foo")]
    [InlineData("/foo/bar")]
    public void Should_add_current_classes_when_full_path_match(string path)
    {
        // Given
        var context = MakeTagHelperContext(
            tagName: "a",
            new TagHelperAttributeList
            {
                { "class", "text-black" },
            });
        var output = MakeTagHelperOutput(
            tagName: "a",
            new TagHelperAttributeList
            {
                { "class", "text-black" },
                { "href", path },
            });

        var options = Options.Create(new TagOptions());
        var helper = new LinkTagHelper(options)
        {
            CurrentClass = "bg-orange no-underline",
            DefaultClass = "bg-white underline",
            MatchStyle = PathMatchStyle.Full,
            ViewContext = MakeViewContext(path),
        };

        // When
        helper.Process(context, output);

        // Then
        output.Attributes["class"].ShouldNotBeNull();

        var classList = output.Attributes["class"].Value as HtmlString;
        classList.ShouldNotBeNull();
        classList.Value.ShouldBe("text-black bg-orange no-underline");
    }

    [Theory]
    [InlineData("/foo")]
    [InlineData("/foo/bar")]
    public void Should_add_current_classes_when_base_path_match(string requestPath)
    {
        // Given
        var context = MakeTagHelperContext(
            tagName: "a",
            new TagHelperAttributeList
            {
                { "class", "text-black" },
            });
        var output = MakeTagHelperOutput(
            tagName: "a",
            new TagHelperAttributeList
            {
                { "class", "text-black" },
                { "href", "/foo" },
            });

        var options = Options.Create(new TagOptions());
        var helper = new LinkTagHelper(options)
        {
            CurrentClass = "bg-orange no-underline",
            DefaultClass = "bg-white underline",
            MatchStyle = PathMatchStyle.Base,
            ViewContext = MakeViewContext(requestPath),
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
            tagName: "a",
            new TagHelperAttributeList
            {
                { "class", "text-black" },
            });
        var output = MakeTagHelperOutput(
            tagName: "a",
            new TagHelperAttributeList
            {
                { "class", "text-black" },
                { "href", "/foo" },
            });

        var options = Options.Create(
            new TagOptions
            {
                IncludeComments = false,
            });
        var helper = new LinkTagHelper(options)
        {
            CurrentClass = "bg-orange no-underline",
            DefaultClass = "bg-white underline",
            ViewContext = MakeViewContext("/foo"),
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
            tagName: "a",
            new TagHelperAttributeList
            {
                { "class", "text-black" },
            });
        var output = MakeTagHelperOutput(
            tagName: "a",
            new TagHelperAttributeList
            {
                { "class", "text-black" },
                { "href", "/foo" },
            });

        var options = Options.Create(
            new TagOptions
            {
                IncludeComments = true,
            });
        var helper = new LinkTagHelper(options)
        {
            CurrentClass = "bg-orange no-underline",
            DefaultClass = "bg-white underline",
            ViewContext = MakeViewContext("/foo"),
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
}
