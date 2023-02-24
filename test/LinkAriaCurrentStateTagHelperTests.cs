namespace Tailwind.Css.TagHelpers;

using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

public class LinkAriaCurrentStateTagHelperTests : TagHelperTestBase
{
    [Fact]
    public void Should_not_throw_when_no_link_context_set()
    {
        // Given
        var context = MakeTagHelperContext(
            tagName: "a",
            new TagHelperAttributeList());
        var output = MakeTagHelperOutput(
            tagName: "a",
            new TagHelperAttributeList());

        var logger = A.Dummy<ILogger<LinkAriaCurrentStateTagHelper>>();
        var options = Options.Create(new TagOptions());
        var helper = new LinkAriaCurrentStateTagHelper(logger, options);

        // When
        var result = () => helper.Process(context, output);

        // Then
        result.ShouldNotThrow();
    }

    [Theory]
    [InlineData(LinkAriaCurrentState.True, "true")]
    [InlineData(LinkAriaCurrentState.Page, "page")]
    [InlineData(LinkAriaCurrentState.Step, "step")]
    [InlineData(LinkAriaCurrentState.False, "false")]
    [InlineData(LinkAriaCurrentState.Location, "location")]
    [InlineData(LinkAriaCurrentState.Date, "date")]
    [InlineData(LinkAriaCurrentState.Time, "time")]
    public void Should_set_attribute_when_enabled(LinkAriaCurrentState state, string attributeValue)
    {
        // Given
        var context = MakeTagHelperContext(
            tagName: "a",
            new TagHelperAttributeList());
        var output = MakeTagHelperOutput(
            tagName: "a",
            new TagHelperAttributeList());

        AddLinkContext(context, isMatch: true);

        var logger = A.Dummy<ILogger<LinkAriaCurrentStateTagHelper>>();
        var options = Options.Create(
            new TagOptions
            {
                IncludeComments = true,
            });
        var helper = new LinkAriaCurrentStateTagHelper(logger, options)
        {
            State = state,
        };

        // When
        helper.Process(context, output);

        // Then
        var result = output.Attributes.FirstOrDefault(a => a.Name == "aria-current");
        result.ShouldNotBeNull();

        result.Value.ShouldBe(attributeValue);
    }

    [Fact]
    public void Should_not_emit_comments_when_setting_is_disabled()
    {
        // Given
        var context = MakeTagHelperContext(
            tagName: "a",
            new TagHelperAttributeList());
        var output = MakeTagHelperOutput(
            tagName: "a",
            new TagHelperAttributeList());

        var logger = A.Dummy<ILogger<LinkAriaCurrentStateTagHelper>>();
        var options = Options.Create(
            new TagOptions
            {
                IncludeComments = false,
            });
        var helper = new LinkAriaCurrentStateTagHelper(logger, options);

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
            new TagHelperAttributeList());
        var output = MakeTagHelperOutput(
            tagName: "a",
            new TagHelperAttributeList());

        var logger = A.Dummy<ILogger<LinkAriaCurrentStateTagHelper>>();
        var options = Options.Create(
            new TagOptions
            {
                IncludeComments = true,
            });
        var helper = new LinkAriaCurrentStateTagHelper(logger, options);

        // When
        helper.Process(context, output);

        // Then
        output.PreElement.GetContent().ShouldBe(
            """
            <!-- ⚠️ Parent context not found for element using aria-current-state="Page" tag helper -->

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
